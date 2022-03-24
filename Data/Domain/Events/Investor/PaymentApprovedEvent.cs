using System;

namespace Common.Data.Domain.Events.Investor
{
    public class PaymentApprovedEvent : IEvent
    {
        public bool IsPaid { get; private set; }
        public decimal AmountRequested { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal BalanceUnpaid { get; private set; }
        public Guid ModifiedBy { get; private set; }

        public PaymentApprovedEvent(bool isPaid, decimal amountRequested, decimal amountPaid, Guid modifiedBy)
        {
            IsPaid = isPaid;
            AmountRequested = amountRequested;
            AmountPaid = amountPaid;
            ModifiedBy = modifiedBy;
            BalanceUnpaid = amountRequested - amountPaid;
        }
    }
}
