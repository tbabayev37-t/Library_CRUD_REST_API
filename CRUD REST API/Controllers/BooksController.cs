using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.QueryDto;
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
        /// <summary>
        /// Butun kitablarin siyahısını sehifeleme və siralama ile getirir.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] BookQueryParameters queryParams)
        {
            var books = await _bookService.GetAllAsync(queryParams);
            return Ok(books);
        }
        /// <summary> ID-e gore tek bir kitabi getirir. </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if(book == null) return NotFound("Kitab tapilmadi!");//404 Not Found
            return Ok(book);
        }
        /// <summary> Yeni kitab yaradir. </summary>
        [HttpPost]
        public async Task<IActionResult>CreateBook(BookCreateDto dto)
        {
                await _bookService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary> Movcud kitab melumatlarini yenileyir. </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID-ler ust-uste dusmur!" });
            await _bookService.UpdateAsync(dto);
            return NoContent(); // 204
        }
        /// <summary> ID-e gore kitabi silir. </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
                await _bookService.DeleteAsync(id);
                return NoContent();//204 No Content
            
        }
    }
}
