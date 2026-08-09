using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<(byte[] FileBytes, string ContentType, string FileName)>DownloadFileAsync(string fileName, string folderName);
    }
}
