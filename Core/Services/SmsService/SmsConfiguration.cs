using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Services.SmsService
{
    public class SmsConfiguration
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string From { get; set; }
    }
}
