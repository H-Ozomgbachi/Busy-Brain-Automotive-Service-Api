using Microsoft.AspNetCore.Authorization;

namespace Common.Api.Configuration.Authorization.Requirement
{
    public class OnlyAdminRequirement : IAuthorizationRequirement
    {
    }
}
