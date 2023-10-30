using DinkToPdf;
using DinkToPdf.Contracts;

namespace SistemaDeCitas.Services
{
    public class GenerarPdf
    {
        private readonly IConverter _converter;
        //Creamos un constructor y le pasamos un parametro de la libreria
        public GenerarPdf(IConverter converter)
        {
           this._converter = converter;//Inicializamos las variables
        }

        public byte[] PdfGenerar(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings {Top = 10 , Bottom = 10 , Left = 10,Right = 10 },
                DocumentTitle = "Reportes del mes Pdf"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = {DefaultEncoding = "utf-8"},
                HeaderSettings = {FontSize = 12, Right = "Page [page] of [toPage]",Line = true, Spacing = 2.812},
                FooterSettings = {FontSize = 12,Line = true, Right = "(C)"+DateTime.Now.Year}
            };
            var documento = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = {objectSettings}
            };
            return _converter.Convert(documento);
        }
    }
}
