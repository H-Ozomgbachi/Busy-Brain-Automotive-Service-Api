namespace Common.Data.Domain
{
    public class SetOriginEvent : IEvent
    {
        public string origin { get; private set; }

        public SetOriginEvent(string origin)
        {
            this.origin = origin;
        }
    }
}