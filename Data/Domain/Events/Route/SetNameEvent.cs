using System;

namespace Common.Data.Domain
{
    public class SetNameEvent : IEvent
    {
        public Guid UniqueID { get; private set; }
        public string name { get; private set; }

        public SetNameEvent(string name, Guid uniqueId)
        {
            this.UniqueID = uniqueId;
            this.name = name;
        }
    }
}