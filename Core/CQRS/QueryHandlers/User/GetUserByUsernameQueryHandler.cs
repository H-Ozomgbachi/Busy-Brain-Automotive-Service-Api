using Common.Core.CQRS.Queries;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, Common.Data.Domain.User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByUsernameQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<Common.Data.Domain.User> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByUsernameAsync(request.Username);
        }    
    }
}
