using Common.Contracts.v1.RepairLabourTime;
using Common.Core.Models;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Core.Services.RepairLabourTimeService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Api.Controllers.v1
{
    [Route("api/v1/failure-components")]
    [ApiController]
    public class FailureComponentController : ControllerBase
    {
        private readonly IFailureComponentService _failureComponentService;

        public FailureComponentController(IFailureComponentService failureComponentService)
        {
            _failureComponentService = failureComponentService;
        }

        [HttpPost]
        public async Task<FailureComponentModel> PostFailureComponent(CreateFailureComponent failureComponent)
        {
            if (User.IsInRole("Admin"))
            {
                Guid activeUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.Name));
                return await _failureComponentService.CreateFailureComponent(failureComponent, activeUserId);
            }
            throw new UnauthorizedAccessException("You're not authorized to carry out this action");
        }

        [HttpGet("{id}")]
        public async Task<FailureComponentModel> GetFailureComponent(int id)
        {
            return await _failureComponentService.GetFailureComponent(id);
        }

        [HttpPut("{id}")]
        public async Task<FailureComponentModel> EditFailureComponent(UpdateFailureComponent failureComponent)
        {
            if (User.IsInRole("Admin"))
            {
                Guid activeUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.Name));
                return await _failureComponentService.UpdateFailureComponent(failureComponent, activeUserId);
            }
            throw new UnauthorizedAccessException("You're not authorized to carry out this action");
        }

        [HttpGet]
        public async Task<PagedListResult<FailureComponentModel>> GetFailureComponents([FromQuery] PaginateFailureComponent paginateFailureComponent)
        {
            return await _failureComponentService.GetFailureComponents(paginateFailureComponent);
        }

        [HttpDelete("{id}")]
        public async Task DeleteFailureComponent(int id)
        {
            await _failureComponentService.DeleteFailureComponent(id);
        }
    }
}
