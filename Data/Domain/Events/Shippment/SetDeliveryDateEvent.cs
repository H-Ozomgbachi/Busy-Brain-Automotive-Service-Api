using System;

namespace Common.Data.Domain
{
    public class SetDeliveryDateEvent : IEvent
    {
        public  DateTime deliveryDate { get; private set; }

        public SetDeliveryDateEvent(DateTime deliveryDate)
        {
            this.deliveryDate = deliveryDate;
        }
    }
}