using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events.BankAccount
{
    public class BankAccountCreatedEvent : IEvent
    {
        public Guid UniqueId { get; private set; }
        public string BankName { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }
        public int InvestorId { get; private set; }
        public DateTime DateAdded { get; private set; }

        public BankAccountCreatedEvent(Guid uniqueId, string bankName, string accountName, string accountNumber, int investorId, DateTime dateAdded)
        {
            UniqueId = uniqueId;
            BankName = bankName;
            AccountName = accountName;
            AccountNumber = accountNumber;
            InvestorId = investorId;
            DateAdded = dateAdded;
        }
    }
}
