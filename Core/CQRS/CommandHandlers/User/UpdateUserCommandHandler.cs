using Common.Core.CQRS.Commands;
using Common.Data.Domain;
using Common.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers
{
    class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if(!string.IsNullOrWhiteSpace(request.FirstName) && !string.IsNullOrWhiteSpace(request.LastName))
            {
                user.SetName(request.FirstName, request.LastName);
            }

            if(!string.IsNullOrWhiteSpace(request.Email))
            {
                user.SetEmail(request.Email);
            }

            if (!string.IsNullOrWhiteSpace(request.Phone) && !string.IsNullOrWhiteSpace(request.CountryCode))
            {
                user.SetPhone(request.Phone, request.CountryCode);
            }

            if (request.PasswordHash != null && request.PasswordSalt != null)
            {
                user.SetPassword(request.PasswordHash, request.PasswordSalt);
            }

            if(request.ForcePasswordReset.HasValue)
            {
                user.SetForcePasswordResetSet(request.ForcePasswordReset.Value);
            }

            if(request.IsDeleted.HasValue)
            {
                user.Delete();
            }

            if (request.Roles != null && request.Roles.Any())
            {
                user.SetRoles(request.Roles);
            }

            if (request.Status.HasValue)
            {
                user.SetStatus((AccountStatus)request.Status.Value);
            }

            if (request.OrganisationId.HasValue)
            {
                user.SetOrganisation(request.OrganisationId);
            }

            return await _userRepository.UpdateAsync(user, request.ChangedBy);
        }
    }
}
