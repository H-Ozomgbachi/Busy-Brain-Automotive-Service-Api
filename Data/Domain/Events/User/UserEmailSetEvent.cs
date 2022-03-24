using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserEmailSetEvent : IEvent
    {
        public string Email { get; set; }

        public UserEmailSetEvent(string email)
        {
            this.Email = email;
        }
    }
}
