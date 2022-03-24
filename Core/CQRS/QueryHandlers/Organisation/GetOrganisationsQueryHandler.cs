using Common.Core.CQRS.Queries;
using Common.Core.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.Organisation
{
    public class GetOrganisationsQueryHandler : IRequestHandler<GetOrganisationsQuery, List<ListItemViewModel>>
    {
        private readonly IConfiguration _config;
        public GetOrganisationsQueryHandler(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<List<ListItemViewModel>> Handle(GetOrganisationsQuery request, CancellationToken cancellationToken)
        {
            List<ListItemViewModel> orgs = new List<ListItemViewModel>();

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "usp_get_organisations";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using var dr = await command.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        var org = new ListItemViewModel
                        {
                          Id =  dr.GetInt32(dr.GetOrdinal("id")),
                          Name =  dr.GetString(dr.GetOrdinal("name"))
                        };
                        orgs.Add(org);
                    }

                }
                return orgs;
            }
        }
    }
}
