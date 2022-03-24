using Common.Contracts.v1.RepairLabourTime;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Core.Services.RepairLabourTimeService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Api.Controllers.v1
{
    [Route("api/v1/failure-components/{failureComponentId}/maintenance-items")]
    [ApiController]
    public class MaintenanceItemController : ControllerBase
    {
        private readonly IMaintenanceItemService _maintenanceItemService;

        public MaintenanceItemController(IMaintenanceItemService maintenanceItemService)
        {
            _maintenanceItemService = maintenanceItemService;
        }

        [HttpPost]
        public async Task<MaintenanceItemModel> PostMaintenanceItem(int failureComponentId, CreateMaintenanceItem maintenanceItem)
        {
            if (User.IsInRole("Admin"))
            {
                Guid activeUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.Name));
                maintenanceItem.FailureComponentId = failureComponentId;
                return await _maintenanceItemService.CreateMaintenanceItem(maintenanceItem, activeUserId);
            }
            throw new UnauthorizedAccessException("You're not authorized to carry out this action");
        }

        [HttpPut("{id}")]
        public async Task<MaintenanceItemModel> UpdateMaintenanceItem(int failureComponentId, UpdateMaintenanceItem maintenanceItem)
        {
            if (User.IsInRole("Admin"))
            {
                Guid activeUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.Name));
                maintenanceItem.FailureComponentId = failureComponentId;
                return await _maintenanceItemService.UpdateMaintenanceItem(maintenanceItem, activeUserId);
            }
            throw new UnauthorizedAccessException("You're not authorized to carry out this action");
        }

        [HttpGet("{id}")]
        public async Task<MaintenanceItemModel> GetMaintenanceItem(int failureComponentId, int id)
        {
            return await _maintenanceItemService.GetMaintenanceItem(failureComponentId, id);
        }

        [HttpGet]
        public async Task<IEnumerable<MaintenanceItemModel>> GetMaintenanceItems(int failureComponentId)
        {
            return await _maintenanceItemService.GetMaintenanceItems(failureComponentId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteMaintenanceItem(int failureComponentId, int id)
        {
            await _maintenanceItemService.DeleteMaintenanceItem(failureComponentId, id);
        }
    }
}
