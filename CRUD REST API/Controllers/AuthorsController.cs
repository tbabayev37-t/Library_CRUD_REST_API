using CRUD_REST_API.Business.DTOs.AuthorDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Business.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        /// <summary>
        /// Butun muelliflerin siyahisini gosterir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
        /// <summary> ID-e gore tek bir muellifi getirir. </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
                var author = await _authorService.GetByIdAsync(id);
                return Ok(author);           
        }
        /// <summary> Yeni muellif yaradir. </summary>
        [HttpPost]
        public async Task<IActionResult>Create(AuthorCreateDto dto)
        {
            await _authorService.CreateAsync(dto);
            return Ok(dto);
        }
        /// <summary> ID-e gore muellifi silir. </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
                await _authorService.DeleteAsync(id);
                return NoContent();

        }
        /// <summary> Movcud muellif melumatlarini yenileyir. </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID-ler ust-uste dusmur!" });
            await _authorService.UpdateAsync(dto);
            return NoContent();  // 204
        }
    }
}