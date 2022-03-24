using Common.Core.Models.RepairLabourTimeModels;
using Common.Core.Models.StandardRepairFeatureModels;
using Common.Core.Services.RepairLabourTimeService;
using Common.Core.Services.StandardRepairFeature;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Api.Controllers.v1
{
    [Route("api/v1/standard-repair")]
    [ApiController]
    public class StandardRepairFeatureController : ControllerBase
    {
        private readonly IStandardRepairService _standardRepairService;
        private readonly IConverter _converter;
        private readonly IMaintenanceItemService _maintenanceItemService;

        public StandardRepairFeatureController(IStandardRepairService standardRepairService, IConverter converter, IMaintenanceItemService maintenanceItemService)
        {
            _standardRepairService = standardRepairService;
            _converter = converter;
            _maintenanceItemService = maintenanceItemService;
        }

        [HttpPost("check-quotation")]
        public async Task<IEnumerable<StandardRepairResult>> GetStandardRepairQuotation(List<string> standardRepairCodes)
        {
            return await _standardRepairService.GetStandardRepairQuotation(standardRepairCodes);
        }

        [HttpPost("download-quotation")]
        public IActionResult DownloadStandardRepairQuotation(List<StandardRepairResult> standardRepairResults)
        {
            byte[] pdf = _converter.Convert(_standardRepairService.DownloadStandardRepairResults(standardRepairResults));
            return File(pdf, "application/pdf", "quotation.pdf");
        }

        [HttpPost("maintenance-items-by-multiple-failure-components")]
        public async Task<IEnumerable<MaintenanceItemModel>> GetMaintenanceItemsByMultipleFailureComponent(List<int> failureComponentIds)
        {
            return await _maintenanceItemService.GetMaintenanceItems(null,failureComponentIds);
        }
    }
}
