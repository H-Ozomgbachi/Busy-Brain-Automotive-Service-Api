using Common.Contracts.Exceptions.Types;
using System;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Common.Core.Services.SmsService
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsConfiguration _smsConfig;
        private readonly ITwilioRestClient _client;

        public SmsSender(SmsConfiguration smsConfig, ITwilioRestClient client)
        {
            _smsConfig = smsConfig;
            _client = client;
        }

        public async Task SendSms(SmsMessage smsMessage)
        {
            try
            {
                await MessageResource.CreateAsync(
                    to: new PhoneNumber(smsMessage.To),
                    from: new PhoneNumber(_smsConfig.From),
                    body: smsMessage.Message,
                    client: _client);
            }
            catch
            {
                throw new BusinessLogicException("Failed to send sms notification");
                //Console.WriteLine("Sms was not sent");
            }
        }
    }
}
