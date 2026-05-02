using System;
using System.IO;

namespace ImportExportSystem.Files
{
    public class FileValidator
    {
        private readonly string[] _allowedExtensions = { ".xlsx", ".csv" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public ValidationResult ValidateFile(string fileName, long fileSize)
        {
            var result = new ValidationResult { IsValid = true };

            var extension = Path.GetExtension(fileName).ToLower();
            if (!Array.Exists(_allowedExtensions, ext => ext == extension))
            {
                result.IsValid = false;
                result.Errors.Add("Invalid file type. Only .xlsx and .csv are allowed.");
            }

            if (fileSize > MaxFileSize)
            {
                result.IsValid = false;
                result.Errors.Add("File size exceeds the maximum limit of 10MB.");
            }

            return result;
        }

        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; } = new();
        }
    }
}