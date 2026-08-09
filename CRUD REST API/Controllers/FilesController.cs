using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Business.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string folderName = "books")
        {
            try
            {
                var fileUrl = await _fileService.UploadFileAsync(file, folderName);
                return Ok(new { Url = fileUrl, Message = "Fayl ugurla elave edildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName, [FromQuery] string folderName = "books")
        {
            try
            {
                var (fileBytes, contentType, name) = await _fileService.DownloadFileAsync(fileName, folderName);
                return File(fileBytes, contentType, name);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
