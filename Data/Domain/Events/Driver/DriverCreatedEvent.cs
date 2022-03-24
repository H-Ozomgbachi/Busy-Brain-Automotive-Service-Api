using System;

namespace Common.Data.Domain.Events.Driver
{
    public class DriverCreatedEvent : IEvent
    {
        public Guid UniqueId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string IdentificationType { get; private set; }
        public string IdentificationNumber { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateAdded { get; private set; }
        public string Email { get; private set; }

        public DriverCreatedEvent(Guid uniqueId, string firstName, string lastName, string identificationType, string identificationNumber, string phone, DateTime dateAdded, string email)
        {
            UniqueId = uniqueId;
            FirstName = firstName;
            LastName = lastName;
            IdentificationType = identificationType;
            IdentificationNumber = identificationNumber;
            Phone = phone;
            DateAdded = dateAdded;
            Email = email;
        }
    }
}
