using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserPhoneSetEvent : IEvent
    {
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public UserPhoneSetEvent(string phone, string countryCode)
        {
            this.Phone = phone;
            this.CountryCode = countryCode;
        }
    }
}
