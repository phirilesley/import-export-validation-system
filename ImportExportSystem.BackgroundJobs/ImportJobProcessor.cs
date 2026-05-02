using Hangfire;
using ImportExportSystem.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ImportExportSystem.BackgroundJobs
{
    public class ImportJobProcessor
    {
        private readonly IImportService _importService;

        public ImportJobProcessor(IImportService importService)
        {
            _importService = importService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task Process(Guid jobId)
        {
            await _importService.ProcessImportJobAsync(jobId);
        }
    }
}