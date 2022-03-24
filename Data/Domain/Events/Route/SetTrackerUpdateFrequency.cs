using System;

namespace Common.Data.Domain
{
    public class SetTrackerUpdateFrequency : IEvent
    {
        public Guid UniqueID { get; private set; }
        public int updateFrequency { get; private set; }

        public SetTrackerUpdateFrequency(int updateFrequency, Guid uniqueId)
        {
            this.updateFrequency = updateFrequency;
            this.UniqueID = uniqueId;
        }
    }
}