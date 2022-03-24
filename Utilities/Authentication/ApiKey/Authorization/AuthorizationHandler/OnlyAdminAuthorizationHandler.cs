using System.Threading.Tasks;
using Common.Api.Configuration.Authorization.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace Common.Api.Configuration.Authorization.AuthorizationHandler
{
    public class OnlyAdminAuthorizationHandler : AuthorizationHandler<OnlyAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyAdminRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
