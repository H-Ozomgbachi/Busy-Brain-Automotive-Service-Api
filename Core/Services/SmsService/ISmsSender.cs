using System.Threading.Tasks;

namespace Common.Core.Services.SmsService
{
    public interface ISmsSender
    {
        Task SendSms(SmsMessage smsMessage);
    }
}
