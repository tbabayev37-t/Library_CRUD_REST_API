using CRUD_REST_API.Business.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryGetDto>> GetAllAsync();
        Task<CategoryGetDto> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto categoryCreateDto);
        Task UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task DeleteAsync(int id);
    }
}
