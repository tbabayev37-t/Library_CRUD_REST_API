using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.OrderItemDto
{
    public class OrderItemCreateDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
