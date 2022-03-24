using System;
using System.Collections.Generic;

namespace Common.Contracts.v1.Account
{
    public class UpdateUserModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public bool? ForcePasswordReset { get;  set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public int? OrganisationId { get; set; }
    }
}
