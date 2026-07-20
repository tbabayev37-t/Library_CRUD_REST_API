using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.QueryDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IBookService
    {
        Task<IEnumerable<BookGetDto>> GetAllAsync(BookQueryParameters queryParams);
        Task<BookGetDto> GetByIdAsync(int id);
        Task CreateAsync(BookCreateDto CreateBookDto);
        Task UpdateAsync(BookUpdateDto UpdateBookDto);
        Task DeleteAsync(int id);
    }
}
