using System;

namespace Common.Data.Domain
{
    public class ShipmentDelayCreatedEvent : IEvent
    {
        public string address { get; private set; }
        public decimal longitude { get; private set; }
        public decimal latitude { get; private set; }
        public decimal duration { get; private set; }
        public string metadata { get; private set; }
        public DateTime created { get; private set; }

        public ShipmentDelayCreatedEvent(string address, decimal longitude, decimal latitude, decimal duration, string metadata, DateTime created)
        {
            this.address = address;
            this.longitude = longitude;
            this.latitude = latitude;
            this.duration = duration;
            this.metadata = metadata;
            this.created = created;
        }
    }
}