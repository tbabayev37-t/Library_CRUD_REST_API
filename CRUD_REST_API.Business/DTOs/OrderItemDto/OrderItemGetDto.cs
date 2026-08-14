using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.OrderItemDto
{
    public class OrderItemGetDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
