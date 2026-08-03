using CRUD_REST_API.Business.DTOs.CategoryDto;
using CRUD_REST_API.Business.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderGetDto>> GetAllAsync();
        Task<OrderGetDto> GetByIdAsync(int id);
        Task CreateAsync(OrderCreateDto orderCreateDto);
        Task UpdateAsync(OrderUpdateDto orderUpdateDto);
        Task DeleteAsync(int id);
    }
}
