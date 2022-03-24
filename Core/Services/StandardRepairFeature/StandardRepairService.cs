using Common.Contracts.Exceptions.Types;
using Common.Core.Models.StandardRepairFeatureModels;
using Common.Core.Services.PDFService;
using Common.Core.Services.PDFService.Utility;
using Common.Core.Services.RepairLabourTimeService;
using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Core.Services.StandardRepairFeature
{
    public class StandardRepairService : IStandardRepairService
    {
        private readonly IMaintenanceItemService _maintenanceItemService;
        private readonly ICustomPDFGenerator _pDFGenerator;

        public StandardRepairService(IMaintenanceItemService maintenanceItemService, ICustomPDFGenerator pDFGenerator)
        {
            _maintenanceItemService = maintenanceItemService;
            _pDFGenerator = pDFGenerator;
        }

        public HtmlToPdfDocument DownloadStandardRepairResults(List<StandardRepairResult> standardRepairResults)
        {
            var html = TemplateGenerator.StandardRepairResultHTMLString(standardRepairResults);
            return _pDFGenerator.GeneratedFile(html, "BBAS");
        }

        public async Task<IEnumerable<StandardRepairResult>> GetStandardRepairQuotation(List<string> standardRepairCodes)
        {
            if (standardRepairCodes.Count == 0)
            {
                throw new BusinessLogicException("You did not select any maintenance item", "You did not select any maintenance item");
            }
            List<StandardRepairResult> standardRepairResults = new List<StandardRepairResult>();

            foreach (var item in standardRepairCodes.Distinct())
            {
                var maintenanceItem = await _maintenanceItemService.GetMaintenanceItemByCode(item);

                var standardRepairResult = new StandardRepairResult
                {
                    RepairName = maintenanceItem.Title,
                    RepairCode = maintenanceItem.Code,
                    RepairAmount = (maintenanceItem.LabourTimeHours * maintenanceItem.CostPerHour)
                };
                standardRepairResults.Add(standardRepairResult);
            }

            return standardRepairResults;
        }
    }
}