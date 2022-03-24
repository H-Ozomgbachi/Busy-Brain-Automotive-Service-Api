using Common.Core.CQRS.Queries.User;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.User
{
    public class DoesUserBelongToOrganisationQueryHandler : IRequestHandler<DoesUserBelongToOrganisationQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public DoesUserBelongToOrganisationQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<bool> Handle(DoesUserBelongToOrganisationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            return user.OrganisationId == request.OrganizationId;
        }
    }
}
