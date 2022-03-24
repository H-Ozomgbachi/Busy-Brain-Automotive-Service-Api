using Common.Core.CQRS.Queries;
using Common.Data.Domain;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Common.Data.Domain.User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<Common.Data.Domain.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByRecordIdAsync(request.ID);
        }
    }
}
