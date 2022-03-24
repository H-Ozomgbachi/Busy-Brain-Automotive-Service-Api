using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserCreatedEvent : IEvent
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Phone { get; private set; }
        public string CountryCode { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public List<string> Roles { get; private set; }
        public bool ForcePasswordReset { get; private set; }
        public Guid PasswordResetToken { get; private set; }
        public DateTime LastModified { get; private set; }
        public int OrganisationId { get; private set; }
        public int PositionInOrganisation { get; private set; }
        public UserCreatedEvent(string firstName, string lastName, string email, string username, string phone, string countryCode, byte[] passwordHash, byte[] passwordSalt, List<string> roles, bool forcePasswordReset, Guid passwordResetToken, int organisationId, int positionInOrganisation)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Phone = phone;           
            CountryCode = countryCode;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Roles = roles;
            ForcePasswordReset = forcePasswordReset;
            PasswordResetToken = passwordResetToken;
            LastModified = DateTime.UtcNow;
            OrganisationId = organisationId;
            PositionInOrganisation = positionInOrganisation;
        }
    }
}
