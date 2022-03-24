using Common.Contracts.v1.Organisation;
using Common.Core.Models;
using System;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public interface IOrganisationService
    {
        Task<OrganisationViewModel> Create(OrganisationPayload org, Guid currentUser);
        Task<OrganisationViewModel> GetById(int id);
    }
}
