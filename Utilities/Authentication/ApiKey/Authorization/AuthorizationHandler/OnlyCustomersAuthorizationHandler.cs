using System.Threading.Tasks;
using Common.Api.Configuration.Authorization.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace Common.Api.Configuration.Authorization.AuthorizationHandler
{
    public class OnlyCustomersAuthorizationHandler : AuthorizationHandler<OnlyCustomersRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyCustomersRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Customer))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
