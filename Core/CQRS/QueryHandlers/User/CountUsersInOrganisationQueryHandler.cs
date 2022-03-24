using Common.Core.CQRS.Queries.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.User
{
    public class CountUsersInOrganisationQueryHandler : IRequestHandler<CountUsersInOrganisationQuery, int>
    {
        private readonly IConfiguration _config;

        public CountUsersInOrganisationQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> Handle(CountUsersInOrganisationQuery request, CancellationToken cancellationToken)
        {
            int numberOfUsers = 0;
            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_count_users_in_organisation";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@org_id", SqlDbType.Int).Value = request.OrganisationId;
                await connection.OpenAsync();

                using var dr = await command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    numberOfUsers = dr.GetInt32(dr.GetOrdinal("users_in_organisation"));
                }
            }
            return numberOfUsers;
        }
    }
}
