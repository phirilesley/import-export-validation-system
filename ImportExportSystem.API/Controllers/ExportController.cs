using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImportExportSystem.Infrastructure.Services;

namespace ImportExportSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly ExcelReaderService _excelReader;
        private readonly PdfExportService _pdfExporter;

        public ExportController(ExcelReaderService excelReader, PdfExportService pdfExporter)
        {
            _excelReader = excelReader;
            _pdfExporter = pdfExporter;
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportExcel()
        {
            // Sample data - in real app, fetch from DB
            var data = new List<Dictionary<string, string>>
            {
                new() { { "Name", "John" }, { "Email", "john@example.com" } },
                new() { { "Name", "Jane" }, { "Email", "jane@example.com" } }
            };

            // For simplicity, return sample Excel
            return File(new byte[0], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export.xlsx");
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> ExportPdf()
        {
            // Sample data
            var data = new List<Dictionary<string, string>>
            {
                new() { { "Name", "John" }, { "Email", "john@example.com" } },
                new() { { "Name", "Jane" }, { "Email", "jane@example.com" } }
            };

            var pdfBytes = _pdfExporter.GeneratePdf(data);
            return File(pdfBytes, "application/pdf", "export.pdf");
        }
    }
}