using Common.Core.Models.StandardRepairFeatureModels;
using DinkToPdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Core.Services.StandardRepairFeature
{
    public interface IStandardRepairService
    {
        Task<IEnumerable<StandardRepairResult>> GetStandardRepairQuotation(List<string> standardRepairCodes);

        HtmlToPdfDocument DownloadStandardRepairResults(List<StandardRepairResult> standardRepairResults);
    }
}
