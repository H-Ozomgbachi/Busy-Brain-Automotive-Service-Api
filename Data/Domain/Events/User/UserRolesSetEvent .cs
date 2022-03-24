using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserRolesSetEvent : IEvent
    {
        public List<string> Roles { get; private set; }

        public UserRolesSetEvent(List<string> roles)
        {
            this.Roles = roles;
        }
    }
}
