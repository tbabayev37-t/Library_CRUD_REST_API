using CRUD_REST_API.Business.DTOs.AuthorDto;
using CRUD_REST_API.Business.DTOs.BookDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuhtorGetDto>> GetAllAsync();
        Task<AuhtorGetDto> GetByIdAsync(int id);
        Task CreateAsync(AuthorCreateDto AuthorCreateDto);
        Task UpdateAsync(AuthorUpdateDto AuthorUpdateDto);
        Task DeleteAsync(int id);
    }
}
