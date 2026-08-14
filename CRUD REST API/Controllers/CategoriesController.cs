using CRUD_REST_API.Business.DTOs.CategoryDto;
using CRUD_REST_API.Business.DTOs.MemberDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Business.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;            
        }
        /// <summary>Butun kateqoriyalari gosterir </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        /// <summary>ID-e gore kateqoriyani gosterir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }
        /// <summary> Yeni kateqoriya yaradir. </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);
            return StatusCode(201, new { message = "Category successfully created." });
        }
        /// <summary> Movcud kateqoriya melumatlarini yenileyir. </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "IDs don't fall on top of each other!" });
            await _categoryService.UpdateAsync(dto);
            return NoContent();
        }
        /// <summary> ID-e gore kateqoriyani silir. </summary>>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
