using AutoMapper;
using CRUD_REST_API.Business.DTOs.AuthorDto;
using CRUD_REST_API.Business.DTOs.CategoryDto;
using CRUD_REST_API.Business.Profiles;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var deletedCategory = await _categoryRepository.GetByIdAsync(id);
            if(deletedCategory == null)
            {
                throw new KeyNotFoundException("Category is not found");
            }
             _categoryRepository.Delete(deletedCategory);
            await _categoryRepository.SaveAsync();
        }

        public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            var allCategory = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryGetDto>>(allCategory);
        }

        public async Task<CategoryGetDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("This category does not exist!");

            return _mapper.Map<CategoryGetDto>(category);
        }

        public async Task UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updatedCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
            if(updatedCategory == null) { throw new KeyNotFoundException("This category does not exist"); }
            _mapper.Map(categoryUpdateDto, updatedCategory);
            _categoryRepository.Update(updatedCategory);
            await _categoryRepository.SaveAsync();
        }
    }
}
