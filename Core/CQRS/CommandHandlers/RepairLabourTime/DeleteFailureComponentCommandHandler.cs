using Common.Core.CQRS.Commands.RepairLabourTime;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.RepairLabourTime
{
    public class DeleteFailureComponentCommandHandler : IRequestHandler<DeleteFailureComponentCommand, bool>
    {
        private readonly IConfiguration _config;

        public DeleteFailureComponentCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> Handle(DeleteFailureComponentCommand request, CancellationToken cancellationToken)
        {
            int rowAffected = 0;
            using SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "usp_failure_component_delete";
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync(cancellationToken);

                command.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;

                rowAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            }
            return (rowAffected == 1);
        }
    }
}
