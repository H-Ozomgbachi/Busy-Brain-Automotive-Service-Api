using System;

namespace Common.Data.Domain
{
    public class Organisation : AggregateRoot
    {
        public int ID { get; private set; }
        public Guid UniqueID { get; private set; }
        public string Name { get; private set; }
        public string ContactEmail { get; private set; }
        public string ReportEmail { get; private set; }
        public string Phone { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? LastModified { get; private set; }
        public string AccountType { get; private set; }

        public Organisation(Guid uniqueId, string name, string contactEmail, string reportEmail,string phone, string accountType)
        {
            RaiseEvent(new OrgainsationCreatedEvent(uniqueId,name,contactEmail,reportEmail, phone, accountType));
        }

        public Organisation(int id,Guid uniqueId, string name, string contactEmail, string reportEmail, string phone, DateTime created, DateTime? modified, string accountType)
        {
            ID = id;
            UniqueID = uniqueId;
            Name = name;
            ContactEmail = contactEmail;
            ReportEmail = reportEmail;
            Phone = phone;
            Created = created;
            LastModified = modified;
            AccountType = accountType;
        }

        protected override void RaiseEvent(IEvent @event)
        {
            ApplyInternal(@event);
            base.RaiseEvent(@event);
        }
        public void Apply(OrgainsationCreatedEvent @event)
        {
            UniqueID = @event.uniqueId;
            Name = @event.name;
            ContactEmail = @event.contactEmail;
            ReportEmail = @event.reportEmail;
            Created = DateTime.UtcNow;
            Phone = @event.phone;
            AccountType = @event.accountType;
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
    }
}
