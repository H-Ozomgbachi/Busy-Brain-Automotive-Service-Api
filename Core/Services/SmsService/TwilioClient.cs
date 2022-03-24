using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Http;

namespace Common.Core.Services.SmsService
{
    public class TwilioClient : ITwilioRestClient
    {
        private readonly ITwilioRestClient _innerClient;
        private readonly SmsConfiguration _smsConfig;

        public TwilioClient(System.Net.Http.HttpClient httpClient, SmsConfiguration smsConfig)
        {
            httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "CustomTwilioRestClient-Demo");

            _smsConfig = smsConfig;
            _innerClient = new TwilioRestClient(
            _smsConfig.AccountSid,
            _smsConfig.AuthToken,
            httpClient: new SystemNetHttpClient(httpClient));
        }

        public Response Request(Request request) => _innerClient.Request(request);
        public Task<Response> RequestAsync(Request request) => _innerClient.RequestAsync(request);
        public string AccountSid => _innerClient.AccountSid;
        public string Region => _innerClient.Region;
        public HttpClient HttpClient => _innerClient.HttpClient;
    }
}
