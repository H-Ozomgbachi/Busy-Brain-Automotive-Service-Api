using MediatR;
using System;
using System.Collections.Generic;

namespace Common.Core.CQRS.Commands
{
    public class RegisterUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public Guid CreatedBy { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public Guid PasswordResetToken { get; set; }
        public bool ForcePasswordReset { get; set; }
        public int OrganisationId { get; set; }
        public int PositionInOrganisation { get; set; }
    }
}
