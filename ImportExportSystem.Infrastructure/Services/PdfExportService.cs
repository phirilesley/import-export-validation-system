using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace ImportExportSystem.Infrastructure.Services
{
    public class PdfExportService
    {
        public byte[] GeneratePdf(IEnumerable<Dictionary<string, string>> data)
        {
            using var memoryStream = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            var table = new PdfPTable(data.First().Keys.Count);
            table.WidthPercentage = 100;

            // Add headers
            foreach (var key in data.First().Keys)
            {
                table.AddCell(new Phrase(key));
            }

            // Add data
            foreach (var row in data)
            {
                foreach (var value in row.Values)
                {
                    table.AddCell(new Phrase(value));
                }
            }

            document.Add(table);
            document.Close();

            return memoryStream.ToArray();
        }
    }
}