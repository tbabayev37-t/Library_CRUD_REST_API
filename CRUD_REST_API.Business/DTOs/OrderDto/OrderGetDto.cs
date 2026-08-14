using CRUD_REST_API.Business.DTOs.OrderItemDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.OrderDto
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemGetDto> Items { get; set; } = new();
    }
}
