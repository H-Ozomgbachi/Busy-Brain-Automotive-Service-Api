using Common.Contracts.v1.RepairLabourTime;
using Common.Core.Models.RepairLabourTimeModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Core.Services.RepairLabourTimeService
{
    public interface IMaintenanceItemService
    {
        Task<MaintenanceItemModel> CreateMaintenanceItem(CreateMaintenanceItem maintenanceItem, Guid modifiedBy);
        Task<MaintenanceItemModel> UpdateMaintenanceItem(UpdateMaintenanceItem maintenanceItem, Guid modifiedBy);
        Task<MaintenanceItemModel> GetMaintenanceItem(int failureComponentId, int id);
        Task<MaintenanceItemModel> GetMaintenanceItemByCode(string code);
        Task DeleteMaintenanceItem(int failureComponentId, int id);
        Task<IEnumerable<MaintenanceItemModel>> GetMaintenanceItems(int? failureComponentId, List<int> failureComponentIds = null);
    }
}
