using Common.Contracts.v1.RepairLabourTime;
using Common.Core.Models;
using Common.Core.Models.RepairLabourTimeModels;
using System;
using System.Threading.Tasks;

namespace Common.Core.Services.RepairLabourTimeService
{
    public interface IFailureComponentService
    {
        Task<FailureComponentModel> CreateFailureComponent(CreateFailureComponent failureComponent, Guid modifiedBy);
        Task<FailureComponentModel> GetFailureComponent(int id);
        Task<FailureComponentModel> UpdateFailureComponent(UpdateFailureComponent failureComponent, Guid modifiedBy);
        Task<PagedListResult<FailureComponentModel>> GetFailureComponents(PaginateFailureComponent paginate);
        Task DeleteFailureComponent(int id);
    }
}
