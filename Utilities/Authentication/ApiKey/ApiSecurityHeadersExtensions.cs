using Microsoft.AspNetCore.Builder;

namespace Common.Api.Configuration
{
    public static class ApiSecurityHeadersExtensions
    {
        public static void UseDefaultSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseReferrerPolicy(opts => opts.SameOrigin());
            app.UseXContentTypeOptions();
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.SameOrigin());
        }
    }
}