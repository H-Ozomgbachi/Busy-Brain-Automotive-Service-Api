using System;

namespace Common.Data.Domain
{
    public class ShipmentCreatedEvent : IEvent
    {
        public int organisationID { get; private set; }
        public string recipientAddress { get; private set; }
        public string recipientName { get; private set; }
        public string recipientPhone { get; private set; }
        public double price { get; private set; }
        public double weight { get; private set; }
        public string trackingNumber { get; private set; }
        public int routeId { get; private set; }
        public int shipperId { get; private set; }
        public int trackerId { get; private set; }
        public DateTime estimatedDelivery { get; private set; }

        public ShipmentCreatedEvent(int organisationID, string recipientAddress, string recipientName, string recipientPhone, double price, double weight, string trackingNumber, int routeId, int shipperId, DateTime estimatedDelivery,int trackerId)
        {
            this.organisationID = organisationID;
            this.recipientAddress = recipientAddress;
            this.recipientName = recipientName;
            this.recipientPhone = recipientPhone;
            this.price = price;
            this.weight = weight;
            this.trackingNumber = trackingNumber;
            this.routeId = routeId;
            this.shipperId = shipperId;
            this.estimatedDelivery = estimatedDelivery;
            this.trackerId = trackerId;
        }
    }
}