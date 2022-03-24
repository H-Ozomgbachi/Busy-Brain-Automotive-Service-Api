using Common.Core.Services.SmsService;
using EmailService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twilio.Clients;

namespace Common.Api.Extensions
{
    public static class EmailAndSmsServiceExtension
    {
        public static IServiceCollection AddEmailingServices(this IServiceCollection services, IConfiguration config)
        {
            // SMS service section
            services.AddHttpClient<ITwilioRestClient, TwilioClient>();

            var smsConfig = config.GetSection("Twilio").Get<SmsConfiguration>();

            services.AddSingleton(smsConfig);

            services.AddScoped<ISmsSender, SmsSender>();

            // EMAIL service section
            var emailConfig = config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            

            return services;
        }
    }
}
