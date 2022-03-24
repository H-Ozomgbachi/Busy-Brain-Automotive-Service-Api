using DinkToPdf;

namespace Common.Core.Services.PDFService
{
    public interface ICustomPDFGenerator
    {
        HtmlToPdfDocument GeneratedFile(string HtmlContent, string documentTitle);
    }
}
