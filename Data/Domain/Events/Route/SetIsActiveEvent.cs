using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain
{
    public class SetIsActiveEvent : IEvent
    {
        public bool IsActive { get; private set; }
        public Guid UniqueID { get; private set; }


        public SetIsActiveEvent(bool isactive, Guid uniqueId)
        {
            this.IsActive = isactive;
            this.UniqueID = uniqueId;
        }
    }
}
