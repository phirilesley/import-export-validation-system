using Hangfire;
using ImportExportSystem.Application.Interfaces;
using ImportExportSystem.Domain.Entities;
using ImportExportSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExportSystem.BackgroundJobs
{
    public class RetryFailedImportsJob
    {
        private readonly ApplicationDbContext _context;
        private readonly IImportService _importService;

        public RetryFailedImportsJob(ApplicationDbContext context, IImportService importService)
        {
            _context = context;
            _importService = importService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task RetryFailedRows(Guid jobId)
        {
            var failedRows = await _context.ImportErrors
                .Where(e => e.ImportJobId == jobId)
                .ToListAsync();

            foreach (var error in failedRows)
            {
                // Attempt to reprocess - simplified
                // In real app, re-validate and save

                var retryLog = new ImportRetryLog
                {
                    Id = Guid.NewGuid(),
                    ImportJobId = jobId,
                    RowNumber = error.RowNumber,
                    ErrorMessage = error.ErrorMessage,
                    RetriedAt = DateTime.UtcNow,
                    Success = true // Assume success for demo
                };

                _context.ImportRetryLogs.Add(retryLog);
            }

            await _context.SaveChangesAsync();
        }
    }
}
