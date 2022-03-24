using Common.Data.Domain;
using System;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public interface IOrganisationRepository
    {
        Task<int> CreateAsync(Organisation org, Guid changedby);
        Task<Organisation> GetByIdAsync(int id);
    }
}
