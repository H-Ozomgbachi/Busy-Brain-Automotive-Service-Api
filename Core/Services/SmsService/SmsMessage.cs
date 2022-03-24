using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Services.SmsService
{
    public class SmsMessage
    {
        public SmsMessage(string to, string message)
        {
            To = to;
            Message = message;
        }

        public string To { get; set; }
        public string Message { get; set; }
    }
}
