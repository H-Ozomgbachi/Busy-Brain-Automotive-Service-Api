using Common.Data.Domain.RepairLabourTime;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public interface IMaintenanceItemRepository
    {
        Task<int> CreateMaintenanceItem(MaintenanceItem maintenanceItem);
        Task<int> UpdateMaintenanceItem(MaintenanceItem maintenanceItem);
        Task<MaintenanceItem> GetMaintenanceItem(int failureComponentId, int id);
        Task<MaintenanceItem> GetMaintenanceItemByCode(string code);
    }
}