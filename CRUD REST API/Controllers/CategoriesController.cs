using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.CategoryDto;
using CRUD_REST_API.Business.DTOs.MemberDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Business.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;            
        }
        /// <summary>Butun kateqoriyalari gosterir </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        /// <summary>ID-e gore kateqoriyani gosterir
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }
        /// <summary> Yeni kateqoriya yaradir. </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);
            return StatusCode(201, new { message = "Category successfully created." });
        }
        /// <summary> Movcud kateqoriya melumatlarini yenileyir. </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "IDs don't fall on top of each other!" });
            await _categoryService.UpdateAsync(dto);
            return NoContent();
        }
        /// <summary> ID-e gore kateqoriyani silir. </summary>>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
