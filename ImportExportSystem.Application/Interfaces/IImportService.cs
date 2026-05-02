using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImportExportSystem.Application.DTOs.Import;

namespace ImportExportSystem.Application.Interfaces
{
    public interface IImportService
    {
        Task<Guid> UploadFileAsync(string fileName, Stream fileStream);
        Task ProcessImportJobAsync(Guid jobId);
        Task<ImportJobStatusDto> GetJobStatusAsync(Guid jobId);
        Task<List<string>> GetJobErrorsAsync(Guid jobId);
        Task<List<Dictionary<string, string>>> GetJobPreviewAsync(Guid jobId);
    }
}
