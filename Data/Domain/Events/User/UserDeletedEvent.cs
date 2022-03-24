using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserDeletedEvent : IEvent
    {
        public bool IsDeleted { get; set; }
    }
}
