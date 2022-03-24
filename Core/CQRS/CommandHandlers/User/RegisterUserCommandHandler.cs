using Common.Core.CQRS.Commands;
using Common.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var user = new Data.Domain.User(request.FirstName,
                request.LastName, 
                request.Email, 
                request.Email,
                request.Phone,
                request.CountryCode,
                request.PasswordHash,
                request.PasswordSalt,
                request.Roles,
                request.ForcePasswordReset,
                request.PasswordResetToken, request.OrganisationId, request.PositionInOrganisation);

            return await _userRepository.CreateAsync(user, request.CreatedBy);
        }
    }
}
