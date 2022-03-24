using System;

namespace Common.Data.Domain
{
    public class OrgainsationCreatedEvent : IEvent
    {
        public Guid uniqueId { get; private set; }
        public string name { get; private set; }
        public string contactEmail { get; private set; }
        public string reportEmail { get; private set; }
        public string phone { get; private set; }
        public string accountType { get; private set; }
        public OrgainsationCreatedEvent(Guid uniqueId, string name, string contactEmail, string reportEmail, string phone, string accountType)
        {
            this.uniqueId = uniqueId;
            this.name = name;
            this.contactEmail = contactEmail;
            this.reportEmail = reportEmail;
            this.phone = phone;
            this.accountType = accountType;
        }
    }
}