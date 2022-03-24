using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Api.Configuration
{
    public static class CustomHealthCheck
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {

            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddSqlServer(configuration.GetValue<string>("ConnectionStrings:DefaultConnection"),
                    name: "primary-DB-check",
                    tags: new string[] { "primarydb" });

            if (!string.IsNullOrWhiteSpace(configuration.GetValue<string>("Bus:configuration:host")))
            {
                var queueSuffix = configuration.GetValue<bool>("AppSettings:IsLive") ? "_prod" : "_dev";

                var priority = new string[] { "basic", "standard", "premium" };
                foreach (var queuePriority in priority)
                {
                    hcBuilder.AddAzureServiceBusQueue("", AppDomain.CurrentDomain.FriendlyName + queuePriority + queueSuffix,
                        name: "messagebus-check-"+ queuePriority,
                        tags: new string[] { "messagebus-"+ queuePriority });
                }
                
            }

            return services;
        }
    }
}
