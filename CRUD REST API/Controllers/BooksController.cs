using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);//200 Ok
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if(book == null) return NotFound("Kitab tapilmadi!");//404 Not Found
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult>CreateBook(BookCreateDto dto)
        {
            try
            {
                await _bookService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut]
        public async Task<IActionResult>Update(BookUpdateDto dto)
        {
            try
            {
                await _bookService.UpdateAsync(dto);
                return NoContent(); //204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //400
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                return NoContent();//204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //400 Bad Request
            }
            
        }
    }
}
