using CRUD_REST_API.Business.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment _env;
        private readonly string[] _allowedExtensions = {".jpg",".jpeg",".png",".pdf"};
        private const long MaxFileSize = 5*1024 * 1024;
        public FileService(IHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if(file == null || file.Length == 0) 
              throw new ArgumentException("Fayl secilmeyib!");
            if (file.Length > MaxFileSize)
                throw new InvalidOperationException("Faylın ölçüsü maksimum 5 MB ola bilər.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                throw new InvalidOperationException("İcazə verilməyən fayl formatı! Yalnız JPG, PNG və PDF yüklənə bilər.");

            var rootPath = Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadPath = Path.Combine(rootPath, "uploads", folderName);

            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folderName}/{uniqueFileName}";
        }
        public async Task<(byte[] FileBytes, string ContentType, string FileName)> DownloadFileAsync(string fileName, string folderName)
        {
            var rootPath = Path.Combine(_env.ContentRootPath, "wwwroot");
            var filePath = Path.Combine(rootPath, "uploads", folderName, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Fayl tapılmadı.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            string contentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            };

            return (memory.ToArray(), contentType, fileName);
        }

        
    }
}
