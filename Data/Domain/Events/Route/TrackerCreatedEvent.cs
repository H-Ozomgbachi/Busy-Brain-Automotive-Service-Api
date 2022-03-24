using System;

namespace Common.Data.Domain
{
    public class TrackerCreatedEvent : IEvent
    {
        public string name { get; }
        public Guid uniqueId { get; }
        public string imie { get; }
        public bool isActive { get; }
        public int updateFrequency { get; }
        public int organisationID { get; }

        public TrackerCreatedEvent(string name, Guid uniqueId, string imie, bool isActive, int updateFrequency, int organisationID)
        {
            this.name = name;
            this.uniqueId = uniqueId;
            this.imie = imie;
            this.isActive = isActive;
            this.updateFrequency = updateFrequency;
            this.organisationID = organisationID;
        }
    }
}