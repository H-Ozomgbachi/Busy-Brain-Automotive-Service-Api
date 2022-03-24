using Common.Core.CQRS.Commands.RepairLabourTime;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.RepairLabourTime
{
    public class DeleteMaintenanceItemCommandHandler : IRequestHandler<DeleteMaintenanceItemCommand, bool>
    {
        private readonly IConfiguration _config;

        public DeleteMaintenanceItemCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> Handle(DeleteMaintenanceItemCommand request, CancellationToken cancellationToken)
        {
            int rowAffected = 0;
            using SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "usp_maintenance_item_delete";
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync(cancellationToken);

                command.Parameters.Add("@failure_component_id", SqlDbType.Int).Value = request.FailureComponentId;
                command.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;

                rowAffected = await command.ExecuteNonQueryAsync(cancellationToken);
            }
            return (rowAffected == 1);
        }
    }
}
