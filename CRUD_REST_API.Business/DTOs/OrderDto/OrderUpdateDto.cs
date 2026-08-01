using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.OrderDto
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
