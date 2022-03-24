namespace Common.Data.Domain
{
    public class SetDestinationEvent : IEvent
    {
        public string destination { get; private set; }

        public SetDestinationEvent(string destination)
        {
            this.destination = destination;
        }
    }
}