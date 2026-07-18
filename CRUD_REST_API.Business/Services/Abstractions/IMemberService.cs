using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.MemberDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberGetDto>> GetAllAsync();
        Task<MemberGetDto> GetByIdAsync(int id);
        Task CreateAsync(MemberCreateDto MemberCreateDto);
        Task UpdateAsync(MemberUpdateDto MemberUpdateDto);
        Task DeleteAsync(int id);
    }
}
