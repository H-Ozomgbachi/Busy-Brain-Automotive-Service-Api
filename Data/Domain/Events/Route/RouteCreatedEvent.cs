using System;

namespace Common.Data.Domain
{
    public class RouteCreatedEvent : IEvent
    {
        public Guid uniqueId { get; private set; }
        public string origin { get; private set; }
        public string destination { get; private set; }
        public int organisationId { get; private set; }
        public RouteCreatedEvent(Guid uniqueId, string origin, string destination, int organisationId)
        {
            this.uniqueId = uniqueId;
            this.origin = origin;
            this.destination = destination;
            this.organisationId = organisationId;
        }
    }
}