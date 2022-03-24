using System;

namespace Common.Data.Domain.Events.Investor
{
    public class InvestorCreatedEvent : IEvent
    {
        public Guid UniqueId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string InvestorCode { get; private set; }
        public DateTime DateOfApplication { get; private set; }
        public DateTime DateAdded { get; private set; }
        public string Email { get; private set; }
        public bool IsApproved { get; private set; }

        public InvestorCreatedEvent(Guid uniqueId, string firstName, string lastName, string phone, string investorCode, DateTime dateOfApplication, DateTime dateAdded, string email, bool isApproved)
        {
            UniqueId = uniqueId;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            InvestorCode = investorCode;
            DateOfApplication = dateOfApplication;
            DateAdded = dateAdded;
            Email = email;
            IsApproved = isApproved;
        }
    }
}
