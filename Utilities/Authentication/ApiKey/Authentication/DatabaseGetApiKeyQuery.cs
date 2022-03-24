using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.Api.Configuration.Authentication
{
    public class DatabaseGetApiKeyQuery : IGetApiKeyQuery
    {
        public readonly IConfiguration _config;
        public DatabaseGetApiKeyQuery(IConfiguration config)
        {
            _config = config;
        }
        public async Task<ApiKey> Execute(string providedApiKey)
        {
            var existingApiKeys = new List<ApiKey>();

            using (var connection = new SqlConnection(_config["ConnectionStrings:AuthConnection"]))
            {
                using var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_GetApiKey_ByKey"
                };

                command.Parameters.Add("@api_key", SqlDbType.NVarChar).Value = providedApiKey;

                await connection.OpenAsync();

                using var dr = await command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    existingApiKeys.Add( new ApiKey
                    (
                        dr.GetGuid(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("owner")),
                        dr.GetString(dr.GetOrdinal("apikey")),
                        (DateTime)dr.GetDateTime(dr.GetOrdinal("created")),
                        dr.GetString(dr.GetOrdinal("roles")).Split(',', StringSplitOptions.RemoveEmptyEntries)

                    ));
                }
            }
            var apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
            apiKeys.TryGetValue(providedApiKey, out var key);
            return await Task.FromResult(key);
        }
    }
}
