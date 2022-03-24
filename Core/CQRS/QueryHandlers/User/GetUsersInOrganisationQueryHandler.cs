using Common.Core.CQRS.Queries.User;
using Common.Core.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.User
{
    public class GetUsersInOrganisationQueryHandler : IRequestHandler<GetUsersInOrganisationQuery, List<ListUserViewModel>>
    {
        private readonly IConfiguration _config;

        public GetUsersInOrganisationQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ListUserViewModel>> Handle(GetUsersInOrganisationQuery request, CancellationToken cancellationToken)
        {
            List<ListUserViewModel> users = new List<ListUserViewModel>();

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_get_users_in_organisation";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    command.Parameters.Add("@orgId", SqlDbType.Int).Value = request.OrganisationId;

                    using var dr = await command.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        var user = new ListUserViewModel
                        {
                            Id = dr.GetGuid(dr.GetOrdinal("id")),
                            FirstName = dr.GetString(dr.GetOrdinal("firstname")),
                            LastName = dr.GetString(dr.GetOrdinal("lastname")),
                            Email = dr.GetString(dr.GetOrdinal("email")),
                            Phone = dr.GetString(dr.GetOrdinal("phone")),
                        };
                        users.Add(user);
                    }
                    return users;
                }
            }
        }
    }
}
