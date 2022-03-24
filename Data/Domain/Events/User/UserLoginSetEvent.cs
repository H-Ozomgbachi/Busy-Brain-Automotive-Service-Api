using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserLoginSetEvent : IEvent
    {
        public DateTime LastLogin { get; set; }
    }
}
