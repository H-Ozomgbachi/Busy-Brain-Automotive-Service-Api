namespace Common.Data.Domain
{
    public class SetShipmentIsActiveEvent : IEvent
    {
        public bool isactive { get; private set; }

        public SetShipmentIsActiveEvent(bool isactive)
        {
            this.isactive = isactive;
        }
    }
}