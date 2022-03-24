using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using Common.Api.Configuration;
using Common.Api.Configuration.Authentication;
using Common.Contracts.Exceptions.Types;
using Common.Api.Configuration.Authorization.AuthorizationHandler;
using Microsoft.FeatureManagement;
using Common.Core.CQRS.Queries;
using Common.Core.CQRS.QueryHandlers;
using Common.Core.Mappers;
using Common.Api.Extensions;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IO;
using Common.Core.Services.PDFService.Utility;

namespace Common.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // The next three lines of codes is to configure data PDF
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddFeatureManagement();

            services.AddHealthChecks();

            services.AddOptions();

            services.AddSingleton<IAuthorizationHandler, OnlyCustomersAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, OnlyAdminAuthorizationHandler>();
            services.AddTransient<IGetApiKeyQuery, DatabaseGetApiKeyQuery>();

            services.AddRepositoriesAndServices();
            services.AddEmailingServices(Configuration);

            services.AddRouting(x => x.LowercaseUrls = true);
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.ConfigureAuthentication(Configuration);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            services.AddMediatR(typeof(GetUserByUsernameQuery).Assembly, typeof(GetUserByUsernameQueryHandler).Assembly);

            services.AddProblemDetails(opts =>
            {
                // Control when an exception is included
                opts.IncludeExceptionDetails = (ctx, ex) =>
                {
                    // Fetch services from HttpContext.RequestServices
                    Log.Error("Error Processing Request", ex);
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };

                opts.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

                // This will map HttpRequestException to the 503 Service Unavailable status code.
                opts.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

                opts.MapToStatusCode<UnauthorizedAccessException>(StatusCodes.Status401Unauthorized);
                // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
                // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
                opts.Map<CoreException>((ex) =>
                {
                    if (ex.ValidationErrors is null || !ex.ValidationErrors.Any())
                    {
                        return new ProblemDetails()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Type = "https://httpstatuses.com/400",
                            Title = ex.FriendlyMessage
                        };
                    }

                    var problemDetails = new ProblemDetails()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Type = "https://httpstatuses.com/400",
                        Title = ex.FriendlyMessage,
                        Detail = JsonConvert.SerializeObject(ex.ValidationErrors)
                    };
                    return problemDetails;
                });

                opts.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultSecurityHeaders();
            
            app.UseProblemDetails();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseCors(_policyName);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Common.Api");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
