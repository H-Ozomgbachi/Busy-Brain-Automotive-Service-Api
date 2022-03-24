namespace Common.Data.Domain
{
    public class SetDriverEvent : IEvent
    {
        public string name { get; private set; }
        public string phone { get; private set; }

        public SetDriverEvent(string name, string phone)
        {
            this.name = name;
            this.phone = phone;
        }
    }
}