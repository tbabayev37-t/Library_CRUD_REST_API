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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            try
            {
                var author = await _authorService.GetByIdAsync(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }             
        }
        [HttpPost]
        public async Task<IActionResult>Create(AuthorCreateDto dto)
        {
            await _authorService.CreateAsync(dto);
            return Ok(dto);
        }
        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                await _authorService.GetByIdAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult>Update(AuthorUpdateDto dto)
        {
            try
            {
                await _authorService.UpdateAsync(dto);
                return NoContent();  //204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //400
            }
        }
    }
}
