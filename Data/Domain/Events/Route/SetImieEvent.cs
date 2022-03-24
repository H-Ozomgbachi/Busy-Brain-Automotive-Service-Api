using System;

namespace Common.Data.Domain
{
    public class SetImieEvent : IEvent
    {
        public Guid UniqueID { get; private set; }
        public string Imie { get; private set; }

        public SetImieEvent(string imie, Guid uniqueId)
        {
            this.Imie = imie;
            this.UniqueID = uniqueId;
        }
    }
}