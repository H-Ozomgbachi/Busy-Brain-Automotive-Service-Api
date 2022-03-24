using Common.Data.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly IConfiguration _config;
        public OrganisationRepository(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<int> CreateAsync(Organisation org, Guid changedby)
        {
            var pendingEvents = org.GetUncommitedEvents();

            if (!pendingEvents.Any())
            {
                return 0;
            }

            var newID = 0;

            using (var connection = new SqlConnection(this._config["ConnectionStrings:DefaultConnection"]))
            {
                using (var cmd = new SqlCommand())
                {
                    await connection.OpenAsync();

                    SqlTransaction transaction = connection.BeginTransaction("organisation_add");
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        foreach (var @event in pendingEvents)
                        {
                            await AddEventToAudit(cmd, @event, org, changedby);
                        }

                        cmd.CommandText = "usp_organisation_add";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //SET COMMAND PARAMETERS  ?? Convert.DBNull
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = org.Name;
                        cmd.Parameters.Add("@contact_email", SqlDbType.NVarChar).Value = org.ContactEmail;
                        cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = org.Phone;
                        cmd.Parameters.Add("@report_email", SqlDbType.NVarChar).Value = org.ReportEmail;
                        cmd.Parameters.Add("@account_type", SqlDbType.NVarChar).Value = org.AccountType;

                        newID = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
            return newID;
        }

        public async Task<Organisation> GetByIdAsync(int id)
        {

            Organisation org = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_Get_Organisation_ById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@org_id", SqlDbType.Int).Value = id;

                    await connection.OpenAsync();

                    using var dr = await command.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        org = new Organisation
                        (
                            dr.GetInt32(dr.GetOrdinal("id")),
                            dr.GetGuid(dr.GetOrdinal("unique_id")),
                            dr.GetString(dr.GetOrdinal("name")),
                            dr.GetString(dr.GetOrdinal("contact_email")),
                            dr.GetString(dr.GetOrdinal("phone")),
                            dr.GetString(dr.GetOrdinal("report_email")),                           
                            dr.GetDateTime(dr.GetOrdinal("created_utc")),
                            dr.GetDateTime(dr.GetOrdinal("last_modified_utc")),
                            dr.GetString(dr.GetOrdinal("account_type"))
                        );
                    }

                }
                return org;
            }
        }

        private async Task AddEventToAudit(SqlCommand cmd, IEvent @event, Organisation org, Guid userID)
        {
            cmd.CommandText = "usp_organisation_event_add";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@event_type", SqlDbType.NVarChar).Value = @event.GetType().Name;
            cmd.Parameters.Add("@event_body", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(@event);
            cmd.Parameters.Add("@created", SqlDbType.DateTime2).Value = DateTime.UtcNow;
            cmd.Parameters.Add("@org_id", SqlDbType.Int).Value = org.ID;
            cmd.Parameters.Add("@changed_by_user_id", SqlDbType.UniqueIdentifier).Value = userID;
            cmd.Parameters.Add("@account_type", SqlDbType.NVarChar).Value = org.AccountType;

            await cmd.ExecuteNonQueryAsync();
            cmd.Parameters.Clear();
        }
    }
}
