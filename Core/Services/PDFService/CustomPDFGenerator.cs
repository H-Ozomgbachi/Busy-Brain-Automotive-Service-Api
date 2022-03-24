using DinkToPdf;
using System.IO;

namespace Common.Core.Services.PDFService
{
    public class CustomPDFGenerator : ICustomPDFGenerator
    {
        public HtmlToPdfDocument GeneratedFile(string HtmlContent, string documentTitle)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = documentTitle
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = HtmlContent,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]" },
                FooterSettings = { FontName = "Arial", FontSize = 9, Center = "Busy Brain Automotive Services",  }
            };

            return new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
        }
    }
}
