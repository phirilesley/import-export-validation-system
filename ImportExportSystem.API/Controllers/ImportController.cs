using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImportExportSystem.Application.Interfaces;
using ImportExportSystem.Application.DTOs.Import;

namespace ImportExportSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = file.OpenReadStream();
            var jobId = await _importService.UploadFileAsync(file.FileName, stream);

            return Ok(new { JobId = jobId, Message = "File uploaded successfully." });
        }

        [HttpPost("process/{jobId}")]
        public async Task<IActionResult> ProcessJob(Guid jobId)
        {
            await _importService.ProcessImportJobAsync(jobId);
            return Ok("Processing started.");
        }

        [HttpGet("{jobId}/status")]
        public async Task<IActionResult> GetJobStatus(Guid jobId)
        {
            var status = await _importService.GetJobStatusAsync(jobId);
            if (status == null) return NotFound();
            return Ok(status);
        }

        [HttpGet("{jobId}/errors")]
        public async Task<IActionResult> GetJobErrors(Guid jobId)
        {
            var errors = await _importService.GetJobErrorsAsync(jobId);
            return Ok(errors);
        }

        [HttpGet("{jobId}/preview")]
        public async Task<IActionResult> GetJobPreview(Guid jobId)
        {
            var preview = await _importService.GetJobPreviewAsync(jobId);
            return Ok(preview);
        }
    }
}