namespace Common.Data.Domain
{
    public class SetRecipientEvent : IEvent
    {
        public  string name { get; private set; }
        public string address { get; private set; }
        public string phone { get; private set; }

        public SetRecipientEvent(string name, string address, string phone)
        {
            this.name = name;
            this.address = address;
            this.phone = phone;
        }
    }
}