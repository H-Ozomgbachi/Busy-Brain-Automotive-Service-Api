using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserStatusSetEvent : IEvent
    {
        public AccountStatus Status { get; set; }

        public UserStatusSetEvent(AccountStatus status)
        {
            this.Status = status;
        }
    }
}
