using ImportExportSystem.Application.Interfaces;
using ImportExportSystem.Domain.Entities;
using ImportExportSystem.Persistence.Context;
using ImportExportSystem.Infrastructure.Services;
using ImportExportSystem.Validation;
using ImportExportSystem.Validation.Validators;
using ImportExportSystem.Application.DTOs.Import;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hangfire;

namespace ImportExportSystem.Application.Services
{
    public class ImportService : IImportService
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelReaderService _excelReader;
        private readonly ValidationEngine _validationEngine;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public ImportService(ApplicationDbContext context, ExcelReaderService excelReader, ValidationEngine validationEngine, IBackgroundJobClient backgroundJobClient)
        {
            _context = context;
            _excelReader = excelReader;
            _validationEngine = validationEngine;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Guid> UploadFileAsync(string fileName, Stream fileStream)
        {
            var jobId = Guid.NewGuid();

            // Save file to storage (simplified, save to temp path)
            var filePath = Path.Combine(Path.GetTempPath(), jobId + ".xlsx");
            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }

            // Create ImportJob
            var job = new ImportJob
            {
                Id = jobId,
                FileName = fileName,
                Status = "Uploaded",
                TotalRows = 0,
                SuccessfulRows = 0,
                FailedRows = 0,
                CreatedAt = DateTime.UtcNow
            };

            _context.ImportJobs.Add(job);
            await _context.SaveChangesAsync();

            // Trigger background job for processing
            _backgroundJobClient.Enqueue<IImportService>(service => service.ProcessImportJobAsync(jobId));

            return job.Id;
        }

        public async Task ProcessImportJobAsync(Guid jobId)
        {
            var job = await _context.ImportJobs.FindAsync(jobId);
            if (job == null) return;

            job.Status = "Processing";
            await _context.SaveChangesAsync();

            // Read Excel
            var filePath = Path.Combine(Path.GetTempPath(), jobId + ".xlsx"); // Assuming saved with jobId
            var rows = _excelReader.ReadExcel(filePath).ToList();

            job.TotalRows = rows.Count;

            foreach (var row in rows)
            {
                var validationResult = _validationEngine.ValidateRow(row);

                if (validationResult.IsValid)
                {
                    var jobRow = new ImportJobRow
                    {
                        Id = Guid.NewGuid(),
                        ImportJobId = jobId,
                        RowNumber = rows.IndexOf(row) + 1,
                        Status = "Success",
                        Data = System.Text.Json.JsonSerializer.Serialize(row),
                        ErrorMessage = null
                    };

                    _context.ImportJobRows.Add(jobRow);
                    job.SuccessfulRows++;
                }
                else
                {
                    var error = new ImportError
                    {
                        Id = Guid.NewGuid(),
                        ImportJobId = jobId,
                        RowNumber = rows.IndexOf(row) + 1,
                        ErrorMessage = string.Join("; ", validationResult.Errors)
                    };

                    _context.ImportErrors.Add(error);
                    job.FailedRows++;
                }
            }

            job.Status = "Completed";
            await _context.SaveChangesAsync();
        }

        public async Task<ImportJobStatusDto> GetJobStatusAsync(Guid jobId)
        {
            var job = await _context.ImportJobs.FindAsync(jobId);
            if (job == null) return null;

            return new ImportJobStatusDto
            {
                Id = job.Id,
                Status = job.Status,
                TotalRows = job.TotalRows,
                SuccessfulRows = job.SuccessfulRows,
                FailedRows = job.FailedRows,
                CreatedAt = job.CreatedAt
            };
        }

        public async Task<List<string>> GetJobErrorsAsync(Guid jobId)
        {
            var errors = await _context.ImportErrors
                .Where(e => e.ImportJobId == jobId)
                .Select(e => $"Row {e.RowNumber}: {e.ErrorMessage}")
                .ToListAsync();

            return errors;
        }

        public async Task<List<Dictionary<string, string>>> GetJobPreviewAsync(Guid jobId)
        {
            var rows = await _context.ImportJobRows
                .Where(r => r.ImportJobId == jobId)
                .Take(10) // Preview first 10
                .Select(r => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(r.Data))
                .ToListAsync();

            return rows.Where(r => r != null).ToList();
        }
    }
}
