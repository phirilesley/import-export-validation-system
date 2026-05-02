using ImportExportSystem.Files.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImportExportSystem.Files
{
    public class FileReaderFactory
    {
        public IFileReader CreateReader(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            return extension switch
            {
                ".xlsx" => new ExcelFileReader(),
                ".csv" => new CsvFileReader(),
                _ => throw new NotSupportedException($"File type {extension} is not supported.")
            };
        }
    }

    public class ExcelFileReader : IFileReader
    {
        public IEnumerable<Dictionary<string, string>> ReadFile(string filePath)
        {
            // Reuse ExcelReaderService logic
            var excelReader = new ImportExportSystem.Infrastructure.Services.ExcelReaderService();
            return excelReader.ReadExcel(filePath);
        }
    }

    public class CsvFileReader : IFileReader
    {
        public IEnumerable<Dictionary<string, string>> ReadFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                var row = new Dictionary<string, string>();
                for (int j = 0; j < headers.Length; j++)
                {
                    row[headers[j]] = values[j];
                }
                yield return row;
            }
        }
    }
}