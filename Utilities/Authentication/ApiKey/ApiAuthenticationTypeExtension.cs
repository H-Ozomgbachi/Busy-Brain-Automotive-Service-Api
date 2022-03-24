using Common.Api.Configuration.Authentication;
using Common.Api.Configuration.Authorization;
using Common.Api.Configuration.Authorization.Requirement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api.Configuration
{
    public static class ApiAuthenticationTypeExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services,  IConfiguration configuration)
        {
            string apiAuthenticationType = configuration.GetValue<string>("AppSettings:authenticationType");

            if (apiAuthenticationType == "apikey")
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                }).AddApiKeySupport(options => { });

                services.AddAuthorization(options =>
                {
                    options.AddPolicy(Policies.OnlyCustomers, policy => policy.Requirements.Add(new OnlyCustomersRequirement()));
                    options.AddPolicy(Policies.OnlyAdmin, policy => policy.Requirements.Add(new OnlyAdminRequirement()));
                });

                services.ConfigureSwaggerApiKeyFeature("Common Api");
            }

            if (apiAuthenticationType == "jwt")
            {
                // Configure Authentication
                services.AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration.GetValue<string>("JwtToken:Issuer"),
                        ValidateAudience = true,
                        ValidAudience = configuration.GetValue<string>("JwtToken:Audience"),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtToken:SigningKey"))),
                        LifetimeValidator = LifetimeValidator
                    };
                });

                services.ConfigureSwaggerJwtFeature("Common Api");
            }
        }

        private static bool LifetimeValidator(DateTime? notBefore,
           DateTime? expires,
           SecurityToken securityToken,
           TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.UtcNow;
        }
    }
}
