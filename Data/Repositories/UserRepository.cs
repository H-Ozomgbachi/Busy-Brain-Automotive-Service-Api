using Common.Data.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<int> CreateAsync(User user, Guid changedby)
        {
            var pendingEvents = user.GetUncommitedEvents();

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

                    SqlTransaction transaction = connection.BeginTransaction("USER_CREATE");
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        foreach (var @event in pendingEvents)
                        {
                            await AddEventToAudit(cmd, @event, user, changedby);
                        }

                        cmd.CommandText = "usp_User_Create";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //SET COMMAND PARAMETERS  ?? Convert.DBNull
                        cmd.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = user.FirstName;
                        cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = user.LastName;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                        cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.Username;
                        cmd.Parameters.Add("@status", SqlDbType.Int).Value = user.Status;
                        cmd.Parameters.Add("@version", SqlDbType.Int).Value = user.Version;
                        cmd.Parameters.Add("@password_hash", SqlDbType.VarBinary).Value = user.PasswordHash;
                        cmd.Parameters.Add("@password_salt", SqlDbType.VarBinary).Value = user.PasswordSalt;
                        cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = user.Phone;
                        cmd.Parameters.Add("@country_code", SqlDbType.NVarChar).Value = user.CountryCode;
                        cmd.Parameters.Add("@roles", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(user.Roles);
                        cmd.Parameters.Add("@password_reset_token", SqlDbType.UniqueIdentifier).Value = user.PasswordResetToken;
                        cmd.Parameters.Add("@last_modified_utc", SqlDbType.DateTime2).Value = user.LastModified;
                        cmd.Parameters.Add("@last_login_utc", SqlDbType.DateTime2).Value = user.LastLogon;
                        cmd.Parameters.Add("@force_password_reset", SqlDbType.Bit).Value = user.ForcePasswordReset;
                        cmd.Parameters.Add("@org_id", SqlDbType.Int).Value = user.OrganisationId;
                        cmd.Parameters.Add("@position_in_organisation", SqlDbType.Int).Value = user.PositionInOrganisation;
                        
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

        public async Task<bool> DoesUsernameExistAsync(string email, Guid exclude)
        {
            var doesExist = false;
            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_does_username_exist";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = email;
                    command.Parameters.Add("@exclude", SqlDbType.UniqueIdentifier).Value =  exclude == Guid.Empty ? Convert.DBNull : exclude;
                    await connection.OpenAsync();

                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            doesExist = dr.HasRows;
                        }
                    }
                }
            }
            return doesExist;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_Get_User_ByGuid";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier).Value = id;                   

                    await connection.OpenAsync();

                    using var dr = await command.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        user = new User
                        (
                            dr.GetGuid(dr.GetOrdinal("id")),
                            dr.GetString(dr.GetOrdinal("firstname")),
                            dr.GetString(dr.GetOrdinal("lastname")),
                            dr.GetString(dr.GetOrdinal("email")),
                            dr.GetString(dr.GetOrdinal("username")),
                            dr.GetString(dr.GetOrdinal("phone")),
                            dr.GetString(dr.GetOrdinal("country_code")),
                            (byte[])dr[dr.GetOrdinal("password_hash")],
                            (byte[])dr[dr.GetOrdinal("password_salt")],
                            JsonConvert.DeserializeObject<List<string>>(dr.GetString(dr.GetOrdinal("roles"))),
                            dr.GetBoolean(dr.GetOrdinal("force_password_reset")),
                            dr.GetGuid(dr.GetOrdinal("password_reset_token")),
                            dr.GetDateTime(dr.GetOrdinal("last_login_utc")),
                            dr.GetDateTime(dr.GetOrdinal("last_modified_utc")),
                            dr.GetInt32(dr.GetOrdinal("version")),
                            dr.GetInt32(dr.GetOrdinal("record_Id")),
                            (AccountStatus)dr[dr.GetOrdinal("status")],
                            dr.GetBoolean(dr.GetOrdinal("is_deleted")),
                            dr.GetInt32(dr.GetOrdinal("organisation_id")),
                            dr.GetInt32(dr.GetOrdinal("position_in_organisation"))
                        );
                    }
                }
            }
            return user;
        }

        public async Task<User> GetByRecordIdAsync(int id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_Get_User_ById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = id;

                    await connection.OpenAsync();

                    using var dr = await command.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        user = new User
                        (
                            dr.GetGuid(dr.GetOrdinal("id")),
                            dr.GetString(dr.GetOrdinal("firstname")),
                            dr.GetString(dr.GetOrdinal("lastname")),
                            dr.GetString(dr.GetOrdinal("email")),
                            dr.GetString(dr.GetOrdinal("username")),
                            dr.GetString(dr.GetOrdinal("phone")),
                            dr.GetString(dr.GetOrdinal("country_code")),
                            (byte[])dr[dr.GetOrdinal("password_hash")],
                            (byte[])dr[dr.GetOrdinal("password_salt")],
                            JsonConvert.DeserializeObject<List<string>>(dr.GetString(dr.GetOrdinal("roles"))),
                            dr.GetBoolean(dr.GetOrdinal("force_password_reset")),
                            dr.GetGuid(dr.GetOrdinal("password_reset_token")),
                            dr.GetDateTime(dr.GetOrdinal("last_login_utc")),
                            dr.GetDateTime(dr.GetOrdinal("last_modified_utc")),
                            dr.GetInt32(dr.GetOrdinal("version")),
                            dr.GetInt32(dr.GetOrdinal("record_Id")),
                            (AccountStatus)dr[dr.GetOrdinal("status")],
                            dr.GetBoolean(dr.GetOrdinal("is_deleted")),
                            dr.GetInt32(dr.GetOrdinal("organisation_id")),
                            dr.GetInt32(dr.GetOrdinal("position_in_organisation"))
                        );
                    }
                }
            }
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_Get_User_ByUsername";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                    await connection.OpenAsync();

                    using var dr = await command.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        user = new User
                        (
                            dr.GetGuid(dr.GetOrdinal("id")),
                            dr.GetString(dr.GetOrdinal("firstname")),
                            dr.GetString(dr.GetOrdinal("lastname")),
                            dr.GetString(dr.GetOrdinal("email")),
                            dr.GetString(dr.GetOrdinal("username")),
                            dr.GetString(dr.GetOrdinal("phone")),
                            dr.GetString(dr.GetOrdinal("country_code")),
                            (byte[])dr[dr.GetOrdinal("password_hash")],
                            (byte[])dr[dr.GetOrdinal("password_salt")],
                            JsonConvert.DeserializeObject<List<string>>(dr.GetString(dr.GetOrdinal("roles"))),
                            dr.GetBoolean(dr.GetOrdinal("force_password_reset")),
                            dr.GetGuid(dr.GetOrdinal("password_reset_token")),
                            dr.GetDateTime(dr.GetOrdinal("last_login_utc")),
                            dr.GetDateTime(dr.GetOrdinal("last_modified_utc")),
                            dr.GetInt32(dr.GetOrdinal("version")),
                            dr.GetInt32(dr.GetOrdinal("record_Id")),
                            (AccountStatus)dr[dr.GetOrdinal("status")],
                            dr.GetBoolean(dr.GetOrdinal("is_deleted")),
                            dr.GetInt32(dr.GetOrdinal("organisation_id")),
                            dr.GetInt32(dr.GetOrdinal("position_in_organisation"))
                        );
                    }
                }
            }
            return user;
        }

        public async Task<int> UpdateAsync(User user, Guid changedby)
        {
            var pendingEvents = user.GetUncommitedEvents();

            if (!pendingEvents.Any())
            {
                return 0;
            }


            using (var connection = new SqlConnection(this._config["ConnectionStrings:DefaultConnection"]))
            {
                using (var cmd = new SqlCommand())
                {
                    await connection.OpenAsync();

                    SqlTransaction transaction = connection.BeginTransaction("USER_UPDATE");
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        foreach (var @event in pendingEvents)
                        {
                            await AddEventToAudit(cmd, @event, user, changedby);
                        }

                        cmd.CommandText = "usp_User_Update";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //SET COMMAND PARAMETERS  ?? Convert.DBNull
                        cmd.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = user.FirstName;
                        cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = user.LastName;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                        cmd.Parameters.Add("@record_Id", SqlDbType.Int).Value = user.RecordId;
                        cmd.Parameters.Add("@status", SqlDbType.Int).Value = user.Status;
                        cmd.Parameters.Add("@version", SqlDbType.Int).Value = user.Version;
                        cmd.Parameters.Add("@password_hash", SqlDbType.VarBinary).Value = user.PasswordHash;
                        cmd.Parameters.Add("@password_salt", SqlDbType.VarBinary).Value = user.PasswordSalt;
                        cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = user.Phone;
                        cmd.Parameters.Add("@country_code", SqlDbType.NVarChar).Value = user.CountryCode;
                        cmd.Parameters.Add("@roles", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(user.Roles);
                        cmd.Parameters.Add("@password_reset_token", SqlDbType.UniqueIdentifier).Value = user.PasswordResetToken;
                        cmd.Parameters.Add("@last_modified_utc", SqlDbType.DateTime2).Value = user.LastModified;
                        cmd.Parameters.Add("@last_login_utc", SqlDbType.DateTime2).Value = user.LastLogon;
                        cmd.Parameters.Add("@force_password_reset", SqlDbType.Bit).Value = user.ForcePasswordReset;
                        cmd.Parameters.Add("@is_deleted", SqlDbType.Bit).Value = user.IsDeleted;
                        cmd.Parameters.Add("@organisation_id", SqlDbType.Int).Value = user.OrganisationId ?? Convert.DBNull ;
                        await cmd.ExecuteNonQueryAsync();
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
            return user.RecordId;
        }

        public async Task<int> ModifyRoleAsync(string[] roles, Guid currentUser)
        {
            int databaseResponse = 0;
            using (var connection = new SqlConnection(this._config["ConnectionStrings:DefaultConnection"]))
            {
                using (var cmd = new SqlCommand())
                {
                    await connection.OpenAsync();

                    SqlTransaction transaction = connection.BeginTransaction("USER_UPDATE");
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;


                        cmd.CommandText = "usp_User_Modify_Role";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //SET COMMAND PARAMETERS  ?? Convert.DBNull
                        
                        cmd.Parameters.Add("@roles", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(roles);
                        cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = currentUser;

                        databaseResponse = await cmd.ExecuteNonQueryAsync();
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
            return databaseResponse;
        }

        private async Task AddEventToAudit(SqlCommand cmd, IEvent @event, User user, Guid userID)
        {
            cmd.CommandText = "usp_User_EventAdd";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@event_type", SqlDbType.NVarChar).Value = @event.GetType().Name;
            cmd.Parameters.Add("@event_body", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(@event);
            cmd.Parameters.Add("@created", SqlDbType.DateTime2).Value = DateTime.UtcNow;
            cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier).Value = user.Id;
            cmd.Parameters.Add("@changed_by_user_id", SqlDbType.UniqueIdentifier).Value = userID;

            await cmd.ExecuteNonQueryAsync();
            cmd.Parameters.Clear();
        }
    }
}
