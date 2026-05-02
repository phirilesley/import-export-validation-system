using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace ImportExportSystem.Infrastructure.Services
{
    public class ExcelReaderService
    {
        public IEnumerable<Dictionary<string, string>> ReadExcel(string filePath)
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1); // First sheet

            var rows = new List<Dictionary<string, string>>();
            var headers = new List<string>();

            // Read headers
            var headerRow = worksheet.Row(1);
            foreach (var cell in headerRow.Cells())
            {
                headers.Add(cell.Value.ToString());
            }

            // Handle empty sheets safely under nullable analysis.
            var lastRowUsed = worksheet.LastRowUsed();
            if (lastRowUsed is null)
            {
                return rows;
            }

            // Read data rows
            for (int row = 2; row <= lastRowUsed.RowNumber(); row++)
            {
                var dataRow = worksheet.Row(row);
                var rowData = new Dictionary<string, string>();
                for (int col = 1; col <= headers.Count; col++)
                {
                    var cell = dataRow.Cell(col);
                    rowData[headers[col - 1]] = cell.Value.ToString();
                }
                rows.Add(rowData);
            }

            return rows;
        }
    }
}
