using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.CQRS.Commands
{
    public class UpdateUserCommand : IRequest<int>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public bool? ForcePasswordReset { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public int? OrganisationId { get; set; }
        public Guid ChangedBy { get; set; }
    }
}
