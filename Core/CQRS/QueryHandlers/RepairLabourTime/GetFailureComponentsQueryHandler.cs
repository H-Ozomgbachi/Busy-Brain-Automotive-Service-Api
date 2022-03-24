using Common.Core.CQRS.Queries.RepairLabourTime;
using Common.Core.Models;
using Common.Core.Models.RepairLabourTimeModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.RepairLabourTime
{
    public class GetFailureComponentsQueryHandler : IRequestHandler<GetFailureComponentsQuery, PagedListResult<FailureComponentModel>>
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;

        public GetFailureComponentsQueryHandler(IConfiguration config, IMediator mediator)
        {
            _config = config;
            _mediator = mediator;
        }

        public async Task<PagedListResult<FailureComponentModel>> Handle(GetFailureComponentsQuery request, CancellationToken cancellationToken)
        {
            List<FailureComponentModel> failureComponents = new List<FailureComponentModel>();

            int totalCount = await _mediator.Send(new FailureComponentsCountQuery());

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_get_failure_components";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@pageNumber", SqlDbType.Int).Value = request.PageNumber;
                command.Parameters.Add("@pageSize", SqlDbType.Int).Value = request.PageSize;

                await connection.OpenAsync(cancellationToken);

                using var dr = await command.ExecuteReaderAsync(cancellationToken);
                while (await dr.ReadAsync(cancellationToken))
                {
                    var failureComponent = new FailureComponentModel
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        Title = dr.GetString(dr.GetOrdinal("title")),
                        AssemblyOrSystemName = dr.GetString(dr.GetOrdinal("assembly_or_system_name")),
                        CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at")),
                    };
                    failureComponents.Add(failureComponent);
                }
            }
            return new PagedListResult<FailureComponentModel>()
            {
                PageNumber = request.PageNumber,
                DocumentCount = totalCount,
                PageSize = request.PageSize,
                Data = failureComponents
            };
        }
    }
}
