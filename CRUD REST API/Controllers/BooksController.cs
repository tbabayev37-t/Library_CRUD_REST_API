using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.QueryDto;
using CRUD_REST_API.Business.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;
        public BooksController(IBookService bookService, IEmailService emailService)
        {
            _bookService = bookService;
            _emailService = emailService;
        }
        /// <summary>
        /// Butun kitablarin siyahısını sehifeleme və siralama ile getirir.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] BookQueryParameters queryParams)
        {
            var books = await _bookService.GetAllAsync(queryParams);
            return Ok(books);
        }
        /// <summary> ID-e gore tek bir kitabi getirir. </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        [ProducesResponseType(typeof(BookGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if(book == null) return NotFound("Kitab tapilmadi!");//404 Not Found
            return Ok(book);
        }
        /// <summary> Yeni kitab yaradir. </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>CreateBook(BookCreateDto dto)
        {
            // 1. Kitab bazaya asinxron şəkildə əlavə olunur
            await _bookService.CreateAsync(dto);

            // 2. Kitab yarandıqdan sonra arxa fonda bildiriş e-poçtu göndərilir (Non-blocking / Fire-and-Forget)
            // 'await' yazılmadığı üçün HTTP sorğusu bu əməliyyatın bitməsini gözləmir.
            _ = Task.Run(() => _emailService.SendEmailAsync(
                "admin@library.com",
                "Yeni Kitab Bildirisi",
                $"Sisteme yeni kitab elave olundu: {dto.Title}"
            ));

            // 3. İstifadəçiyə dərhal 201 Created status kodu qaytarılır
            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary> Movcud kitab melumatlarini yenileyir. </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID-ler ust-uste dusmur!" });
            await _bookService.UpdateAsync(dto);
            return NoContent(); // 204
        }
        /// <summary> ID-e gore kitabi silir. </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
                await _bookService.DeleteAsync(id);
                return NoContent();//204 No Content       
        }
        /// <summary>
        /// Dinamik axtarış, filtrləmə, sıralama və səhifələmə endpoint-i.
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DynamicSearch([FromQuery] BookQueryParameters queryParams)
        {
            var result = await _bookService.GetAllAsync(queryParams);
            return Ok(result);
        }
        /// <summary>
        /// Transaksiya ile create endpoint-i.
        /// </summary>
        [HttpPost("create-with-transaction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateWithTransaction([FromBody] BookCreateDto dto)
        {
            try
            {
                var result = await _bookService.CreateBookWithAuthorLogAsync(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }
}
