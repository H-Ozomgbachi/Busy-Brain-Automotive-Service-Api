using Common.Core.CQRS.Queries;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers
{
    public class DoesUserEmailExistQueryHandler : IRequestHandler<DoesUserEmailExistQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public DoesUserEmailExistQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<bool> Handle(DoesUserEmailExistQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.DoesUsernameExistAsync(request.Email, request.ExcludeUserId);
        }
    }
}
