using Common.Core.CQRS.Queries.RepairLabourTime;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.RepairLabourTime
{
    public class FailureComponentsCountQueryHandler : IRequestHandler<FailureComponentsCountQuery, int>
    {
        private readonly IConfiguration _config;

        public FailureComponentsCountQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> Handle(FailureComponentsCountQuery request, CancellationToken cancellationToken)
        {
            int totalCount = 0;
            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_failure_components_count";
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync(cancellationToken);

                using var dr = await command.ExecuteReaderAsync(cancellationToken);
                while (await dr.ReadAsync(cancellationToken))
                {
                    totalCount = dr.GetInt32(dr.GetOrdinal("total_count"));
                }
            }
            return totalCount;
        }
    }
}
