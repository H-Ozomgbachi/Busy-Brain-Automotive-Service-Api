using Common.Core.CQRS.Queries;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers
{
    public class GetUserByGuidQueryHandler : IRequestHandler<GetUserByGuidQuery, Common.Data.Domain.User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByGuidQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<Common.Data.Domain.User> Handle(GetUserByGuidQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(request.ID);
        }
    }
}
