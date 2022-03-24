using System;

namespace Common.Data.Domain
{
    public class ShipperCreatedEvent : IEvent
    {
        public Guid uniqueId { get; private set; }
        public string name { get; private set; }
        public string contactEmail { get; private set; }
        public string phone { get; private set; }
        public string officeAddress { get; private set; }
        public int organisationId { get; private set; }
        public ShipperCreatedEvent(Guid uniqueId, string name, string contactEmail, string phone, string officeAddress, int organisationId)
        {
            this.uniqueId = uniqueId;
            this.name = name;
            this.contactEmail = contactEmail;
            this.phone = phone;
            this.officeAddress = officeAddress;
            this.organisationId = organisationId;
        }
    }
}