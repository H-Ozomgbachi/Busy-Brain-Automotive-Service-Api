using Common.Contracts.Exceptions.Types;
using Common.Core.CQRS.Commands.User;
using Common.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.User
{
    public class ModifyUserRoleCommandHandler : IRequestHandler<ModifyUserRoleCommand, int>
    {
        private readonly IUserRepository _repository;

        public ModifyUserRoleCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(ModifyUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.UniqueId);

            if (user == null)
            {
                throw new BusinessLogicException("Requested user not found!");
            }
            return await _repository.ModifyRoleAsync(request.Roles, request.UniqueId);
        }
    }
}
