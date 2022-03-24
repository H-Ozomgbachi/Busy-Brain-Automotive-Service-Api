using Common.Contracts.Exceptions.Types;
using Common.Data.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Data.Domain
{
    public class User : AggregateRoot
    {
        public Guid Id { get; private set; }
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
        public DateTime LastLogon { get; private set; }
        public DateTime LastModified { get; private set; }        
        public bool IsDeleted { get; private set; }
        public int Version { get; private set; }
        public AccountStatus Status { get; private set; }
        public int RecordId { get; private set; }
        public int? OrganisationId { get; private set; }
        public int PositionInOrganisation { get; private set; }

        public User(Guid id, string firstName, string lastName,string email, string username,string phone, string countryCode, byte[] passwordHash, byte[] passwordSalt, List<string> roles,bool forcePasswordReset,Guid passwordResetToken, DateTime lastlogon, DateTime lastModified, int version,int recordId,AccountStatus status, bool isdeleted, int? organisationId, int positionInOrganisation)
        {
            Id = id;
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
            LastLogon = lastlogon;
            LastModified = lastModified;
            Version = version;
            Status = status;
            RecordId = recordId;
            IsDeleted = isdeleted;
            OrganisationId = organisationId;
            PositionInOrganisation = positionInOrganisation;
        }

        public User(string firstName, string lastName, string email, string username, string phone, string countryCode, byte[] passwordHash, byte[] passwordSalt, List<string> roles, bool forcePasswordReset, Guid passwordResetToken, int organisationId, int positionInOrganisation)
        {
            RaiseEvent(new UserCreatedEvent(firstName, lastName, email, username, phone,countryCode,passwordHash,passwordSalt,roles,forcePasswordReset,passwordResetToken, organisationId, positionInOrganisation));
        }

        #region Methods
        public void SetName(string firstName, string lastName)
        {
            if (this.IsDeleted)
            {
                throw new ResourceDeletedException("Cannot alter a deleted User");
            }
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                RaiseEvent(new UserNameSetEvent(firstName,lastName));
            }
            else
            {
                throw new DomainValidationException("FirstName or LastName cannot be null or empty");
            }
        }
        public void SetPassword(byte[] passwordHash, byte[] passwordSalt)
        {
            if (this.IsDeleted)
            {
                throw new ResourceDeletedException("Cannot alter a deleted User");
            }
            if (passwordHash != null && passwordSalt != null)
            {
                RaiseEvent(new UserPasswordSetEvent(passwordHash,passwordSalt));
            }
            else
            {
                throw new DomainValidationException("Password cannot be null or empty");
            }
        }
        public void SetEmail(string email)
        {

            if (this.IsDeleted)
            {
                throw new ResourceDeletedException("Cannot alter a deleted user");
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                RaiseEvent(new UserEmailSetEvent(email));
            }
            else
            {
                throw new DomainValidationException("Email cannot be null or empty");
            }
        }       
        public void Delete()
        {
            if (!IsDeleted)
            {
                RaiseEvent(new UserDeletedEvent
                {
                    IsDeleted = true
                });
            }
        }
        public void SetStatus(AccountStatus status)
        {
            if (this.IsDeleted)
            {
                throw new ResourceDeletedException("Cannot set a deleted user status");
            }
            
           RaiseEvent(new UserStatusSetEvent(status));
        }
        public void SetPhone(string phone, string countryCode)
        {
            if (this.IsDeleted)
            {
                throw new ResourceDeletedException("Cannot alter a deleted User");
            }
            if (!string.IsNullOrWhiteSpace(phone) && !string.IsNullOrWhiteSpace(countryCode))
            {
                RaiseEvent(new UserPhoneSetEvent(phone, countryCode));
            }
            else
            {
                throw new DomainValidationException("phone or countryCode cannot be null or empty");
            }
        }
        public void SetLastLogin()
        {
            if (!IsDeleted)
            {
                RaiseEvent(new UserLoginSetEvent
                {
                     LastLogin = DateTime.UtcNow
                });
            }
        }
        public void SetForcePasswordResetSet(bool forceReset)
        {
            if (!IsDeleted)
            {
                RaiseEvent(new UserForcePasswordResetSetEvent
                {
                    ForcePasswordReset = forceReset
                });
            }
        }

        public void SetOrganisation(int? organisationId)
        {
            if (!IsDeleted)
            {
                RaiseEvent(new UserOrganisationSetEvent
                {
                    OrganisationId = organisationId
                });
            }
        }
        public void SetRoles(List<string> roles)
        {

            if (this.IsDeleted)
            {
                throw new ResourceDeletedException("Cannot alter a deleted user");
            }
            if (roles.Any())
            {
                RaiseEvent(new UserRolesSetEvent(roles));
            }
            else
            {
                throw new DomainValidationException("User roles cannot be null or empty");
            }
        }
        #endregion

        #region Events
        protected override void RaiseEvent(IEvent @event)
        {
            ApplyInternal(@event);
            base.RaiseEvent(@event);
        }
        public void Apply(UserCreatedEvent @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Email = @event.Email;
            Username = @event.Username;
            Phone = @event.Phone;
            CountryCode = @event.CountryCode;
            PasswordHash = @event.PasswordHash;
            PasswordSalt = @event.PasswordSalt;
            Roles = @event.Roles;
            ForcePasswordReset = @event.ForcePasswordReset;
            PasswordResetToken = @event.PasswordResetToken;            
            LastModified = @event.LastModified;
            Status = AccountStatus.Active;
            LastLogon = DateTime.UtcNow;
            OrganisationId = @event.OrganisationId;
            Version++;
            PositionInOrganisation = @event.PositionInOrganisation;

        }
        public void Apply(UserNameSetEvent @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            LastModified = DateTime.UtcNow;
            Version++;
        }
        public void Apply(UserPhoneSetEvent @event)
        {
            Phone = @event.Phone;
            CountryCode = @event.CountryCode;
            LastModified = DateTime.UtcNow;
            Version++;
        }        
        public void Apply(UserDeletedEvent @event)
        {
            this.IsDeleted = @event.IsDeleted;
            LastModified = DateTime.UtcNow;

        }
        public void Apply(UserStatusSetEvent @event)
        {
            this.Status = @event.Status;
            Version++;
        }
        public void Apply(UserEmailSetEvent @event)
        {
            Email = @event.Email;
            LastModified = DateTime.UtcNow;
            Version++;
        }
        public void Apply(UserPasswordSetEvent @event)
        {
            PasswordSalt = @event.PasswordSalt;
            PasswordHash = @event.PasswordHash;
            LastModified = DateTime.UtcNow;
            Version++;
        }
        public void Apply(UserForcePasswordResetSetEvent @event)
        {
            this.ForcePasswordReset = @event.ForcePasswordReset;
            LastModified = DateTime.UtcNow;
            Version++;
        }

        public void Apply(UserOrganisationSetEvent @event)
        {
            this.OrganisationId = @event.OrganisationId;
            LastModified = DateTime.UtcNow;
            Version++;
        }
        public void Apply(UserLoginSetEvent @event)
        {
            this.LastLogon = @event.LastLogin;
            LastModified = DateTime.UtcNow;
            Version++;
        }
        public void Apply(UserRolesSetEvent @event)
        {
            this.Roles = @event.Roles;
            LastModified = DateTime.UtcNow;
            Version++;
        }
        private void ApplyInternal(IEvent @event)
        {
            var method = GetType().GetMethod(nameof(Apply), new[] { @event.GetType() });
            if (method is null)
            {
                throw new NotSupportedException($"{@event.GetType().Name} is not supported");
            }
            method.Invoke(this, new[] { @event });
        }
        #endregion
    }
}
