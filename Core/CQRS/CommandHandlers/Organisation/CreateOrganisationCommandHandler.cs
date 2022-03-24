using Common.Core.CQRS.Commands.Organisation;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.Organisation
{
    public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, int>
    {
        private readonly IOrganisationRepository _repository;

        public CreateOrganisationCommandHandler(IOrganisationRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {

            var org = new Data.Domain.Organisation(request.UniqueID,request.Name,request.ContactEmail,request.ReportEmail,request.Phone, request.AccountType);

            return await _repository.CreateAsync(org, request.CreatedBy);
        }
    }
}
