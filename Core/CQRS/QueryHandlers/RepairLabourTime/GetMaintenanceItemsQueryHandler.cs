using Common.Contracts.Exceptions.Types;
using Common.Core.CQRS.Queries.RepairLabourTime;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.RepairLabourTime
{
    public class GetMaintenanceItemsQueryHandler : IRequestHandler<GetMaintenanceItemsQuery, IEnumerable<MaintenanceItemModel>>
    {
        private readonly IConfiguration _config;
        private readonly IUtilityService _utilityService;

        public GetMaintenanceItemsQueryHandler(IConfiguration config, IUtilityService utilityService)
        {
            _config = config;
            _utilityService = utilityService;
        }

        public async Task<IEnumerable<MaintenanceItemModel>> Handle(GetMaintenanceItemsQuery request, CancellationToken cancellationToken)
        {
            List<MaintenanceItemModel> maintenanceItems = new List<MaintenanceItemModel>();

            string storedProcedureToUse = DecideCorrectSproc(request);

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = storedProcedureToUse;
                command.CommandType = CommandType.StoredProcedure;

                DecideCorrectSprocParameters(storedProcedureToUse, command, request);

                await connection.OpenAsync(cancellationToken);

                using var dr = await command.ExecuteReaderAsync(cancellationToken);
                while (await dr.ReadAsync(cancellationToken))
                {
                    var maintenanceItem = new MaintenanceItemModel
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        Title = dr.GetString(dr.GetOrdinal("title")),
                        Code = dr.GetString(dr.GetOrdinal("code")),
                        LabourTimeHours = dr.GetInt32(dr.GetOrdinal("labour_time_hours")),
                        TruckModel = dr.GetString(dr.GetOrdinal("truck_model")),
                        CostPerHour = dr.GetDecimal(dr.GetOrdinal("cost_per_hour")),
                        FailureComponentId = dr.GetInt32(dr.GetOrdinal("failure_component_id"))
                    };
                    maintenanceItems.Add(maintenanceItem);
                }
            }
            return maintenanceItems;
        }

        private void DecideCorrectSprocParameters(string storedProcedureToUse, SqlCommand command, GetMaintenanceItemsQuery request)
        {
            if (storedProcedureToUse == "usp_get_maintenance_items_by_failure_component")
            {
                command.Parameters.Add("@failure_component_id", SqlDbType.Int).Value = request.FailureComponentId;
            }
            else
            {
                command.Parameters.Add("@failure_component_ids", SqlDbType.NVarChar).Value = _utilityService.MergeIntListToString(request.FailureComponentIds);
            }
        }

        private string DecideCorrectSproc(GetMaintenanceItemsQuery request)
        {
            if (request.FailureComponentIds == null && request.FailureComponentId.HasValue)
            {
                return "usp_get_maintenance_items_by_failure_component";
            }
            else if (request.FailureComponentIds != null && request.FailureComponentId == null)
            {
                return "usp_get_maintenance_items_by_multiple_failure_component";
            }
            else
            {
                throw new BusinessLogicException("You must select failure component before getting maintenance items", "You must select failure component before getting maintenance items");
            }
        }
    }
}
