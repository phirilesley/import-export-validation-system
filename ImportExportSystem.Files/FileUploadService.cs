using System;
using System.IO;
using System.Threading.Tasks;

namespace ImportExportSystem.Files
{
    public class FileUploadService
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public FileUploadService()
        {
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var fileId = Guid.NewGuid().ToString();
            var filePath = Path.Combine(_uploadPath, fileId + Path.GetExtension(fileName));

            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }

            return filePath;
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}