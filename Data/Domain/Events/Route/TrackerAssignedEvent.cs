using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events.Route
{
    public class TrackerAssignedEvent : IEvent
    {
        public string name { get; }
        public Guid uniqueId { get; }
        public string imie { get; }
        public bool isActive { get; }
        public int updateFrequency { get; }
        public int organisationID { get; }
        public int Id { get; }
        public DateTime lastModified { get; }
        public DateTime created { get; }

        public TrackerAssignedEvent(string name, Guid uniqueId, string imie, bool isActive, int updateFrequency, int organisationID, int id, DateTime lastModified, DateTime created)
        {
            this.name = name;
            this.uniqueId = uniqueId;
            this.imie = imie;
            this.isActive = isActive;
            this.updateFrequency = updateFrequency;
            this.organisationID = organisationID;
            this.Id = id;
            this.lastModified = lastModified;
            this.created = created;
        }
    }
}
