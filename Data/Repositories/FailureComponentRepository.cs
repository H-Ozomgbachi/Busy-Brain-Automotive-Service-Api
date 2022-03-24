using Common.Data.Domain.RepairLabourTime;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public class FailureComponentRepository : IFailureComponentRepository
    {
        private readonly IConfiguration _config;

        public FailureComponentRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> CreateFailureComponent(FailureComponent failureComponent)
        {
            int newId = 0;

            using (var connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using var cmd = new SqlCommand();
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction("failure_add");

                try
                {
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.CommandText = "usp_failure_component_add";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = failureComponent.Title;
                    cmd.Parameters.Add("@assembly_or_system_name", SqlDbType.NVarChar).Value = failureComponent.AssemblyOrSystemName;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime).Value = failureComponent.CreatedAt;
                    cmd.Parameters.Add("@modified_by", SqlDbType.UniqueIdentifier).Value = failureComponent.ModifiedBy;

                    newId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
            return newId;
        }

        public async Task<FailureComponent> GetFailureComponent(int id)
        {
            FailureComponent failureComponent = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_get_failure_component";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                await connection.OpenAsync();

                using var dr = await command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    failureComponent = new FailureComponent(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("title")),
                        dr.GetString(dr.GetOrdinal("assembly_or_system_name")),
                        dr.GetDateTime(dr.GetOrdinal("created_at")),
                        dr.GetGuid(dr.GetOrdinal("modified_by")));
                }
            }
            return failureComponent;
        }

        public async Task<int> UpdateFailureComponent(FailureComponent failureComponent)
        {
            int databaseResponse = 0;
            using SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            using SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            SqlTransaction transaction = connection.BeginTransaction("failure_component_update");
            try
            {
                command.Connection = connection;
                command.Transaction = transaction;
                command.CommandText = "usp_failure_component_update";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = failureComponent.Id;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = failureComponent.Title;
                command.Parameters.Add("@assembly_or_system_name", SqlDbType.NVarChar).Value = failureComponent.AssemblyOrSystemName;
                command.Parameters.Add("@created_at", SqlDbType.DateTime).Value = failureComponent.CreatedAt;
                command.Parameters.Add("@modified_by", SqlDbType.UniqueIdentifier).Value = failureComponent.ModifiedBy;

                databaseResponse = await command.ExecuteNonQueryAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
            return databaseResponse;
        }
    }
}
