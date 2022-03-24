namespace Common.Data.Domain
{
    public class ShipmentDeletedEvent : IEvent
    {
        public bool IsDeleted { get; set; } = true;
    }
}