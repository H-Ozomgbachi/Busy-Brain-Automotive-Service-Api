using System;

namespace Common.Data.Domain
{
    public class ShipmentTrackingCreatedEvent : IEvent
    {
        public string address { get; private set; }
        public decimal longitude { get; private set; }
        public decimal latitude { get; private set; }
        public decimal distance { get; private set; }
        public string metadata { get; private set; }
        public DateTime created { get; private set; }

        public ShipmentTrackingCreatedEvent(string address, decimal longitude, decimal latitude, decimal distance, string metadata, DateTime created)
        {
            this.address = address;
            this.longitude = longitude;
            this.latitude = latitude;
            this.distance = distance;
            this.metadata = metadata;
            this.created = created;
        }
    }
}