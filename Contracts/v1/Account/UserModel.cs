using System;
using System.Collections.Generic;

namespace Common.Contracts.v1.Account
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public List<string> Roles { get; set; }
        public int RecordId { get; set; }
        public int PositionInOrganisation { get; set; }
        public int? OrganisationId { get; set; }
    }
}
