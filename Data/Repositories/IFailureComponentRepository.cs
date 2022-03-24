using Common.Data.Domain.RepairLabourTime;
using System;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public interface IFailureComponentRepository
    {
        Task<int> CreateFailureComponent(FailureComponent failureComponent);
        Task<int> UpdateFailureComponent(FailureComponent failureComponent);
        Task<FailureComponent> GetFailureComponent(int id);
    }
}
