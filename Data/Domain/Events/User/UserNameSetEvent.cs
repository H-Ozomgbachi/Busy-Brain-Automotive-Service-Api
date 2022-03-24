using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserNameSetEvent : IEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserNameSetEvent(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
