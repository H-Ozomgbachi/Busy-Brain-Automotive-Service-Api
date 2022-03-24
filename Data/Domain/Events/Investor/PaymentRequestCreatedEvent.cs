using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events.Investor
{
    public class PaymentRequestCreatedEvent : IEvent
    {
        public Guid UniqueId { get; private set; }
        public int InvestorId { get; private set; }
        public decimal AmountRequested { get; private set; }
        public bool IsPaid { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal BalanceUnpaid { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public DateTime DateAdded { get; private set; }

        public PaymentRequestCreatedEvent(Guid uniqueId, int investorId, decimal amountRequested, bool isPaid, decimal amountPaid, decimal balanceUnpaid, Guid modifiedBy, DateTime dateAdded)
        {
            UniqueId = uniqueId;
            InvestorId = investorId;
            AmountRequested = amountRequested;
            IsPaid = isPaid;
            AmountPaid = amountPaid;
            BalanceUnpaid = balanceUnpaid;
            ModifiedBy = modifiedBy;
            DateAdded = dateAdded;
        }
    }
}
