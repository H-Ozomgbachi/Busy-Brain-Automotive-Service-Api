using Common.Core.CQRS.Queries;
using Common.Core.Models;
using Common.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.Organisation
{
    public class GetOrganisationQueryHandler : IRequestHandler<GetOrganisationQuery, OrganisationViewModel>
    {
        private readonly IOrganisationRepository _repository;

        public GetOrganisationQueryHandler(IOrganisationRepository repository)
        {
            _repository = repository;
        }
        public async Task<OrganisationViewModel> Handle(GetOrganisationQuery request, CancellationToken cancellationToken)
        {
            var org = await _repository.GetByIdAsync(request.Id);

            return new OrganisationViewModel
            {
                ID = org.ID,
                Name = org.Name,
                ContactEmail = org.ContactEmail,
                ReportEmail = org.ReportEmail,
                Phone = org.Phone,
                Created = org.Created,
                LastModified = org.LastModified,
                UniqueID = org.UniqueID,
                AccountType = org.AccountType
                
            };
        }
    }
}
