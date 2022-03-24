using Common.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Controllers.v1
{
    [Authorize(Roles = "Admin")]
    [Route("api/v1/utilities")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        private readonly IUtilityService _utilityService;

        public UtilitiesController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }
    }
}
