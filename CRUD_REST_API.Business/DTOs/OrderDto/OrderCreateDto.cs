using CRUD_REST_API.Business.DTOs.OrderItemDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.OrderDto
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> Items { get; set; } = new();
    }
}
