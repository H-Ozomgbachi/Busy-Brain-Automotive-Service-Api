using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserForcePasswordResetSetEvent : IEvent
    {
        public bool ForcePasswordReset { get; set; }
    }
}
