using Common.Core.CQRS.Queries.User;
using Common.Core.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<ListUserViewModel>>
    {
        private readonly IConfiguration _config;

        public GetUsersQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ListUserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<ListUserViewModel> users = new List<ListUserViewModel>();

            using(SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_get_users";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using var dr = await command.ExecuteReaderAsync();
                    while(await dr.ReadAsync())
                    {
                        var user = new ListUserViewModel
                        {
                            Id = dr.GetGuid(dr.GetOrdinal("id")),
                            FirstName = dr.GetString(dr.GetOrdinal("firstname")),
                            LastName = dr.GetString(dr.GetOrdinal("lastname")),
                            Email = dr.GetString(dr.GetOrdinal("email")),
                            Phone = dr.GetString(dr.GetOrdinal("phone")),
                            Roles = JsonConvert.DeserializeObject<List<string>>(dr.GetString(dr.GetOrdinal("roles")))
                        };
                        users.Add(user);
                    }
                    return users;
                }
            }
        }
    }
}
