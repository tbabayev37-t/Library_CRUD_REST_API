using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } 
        public decimal UnitPrice {  get; set; }
        public Order Order { get; set; } = null!;
        public int OrderId {  get; set; }
        public Book Book { get; set; } = null!;
        public int BookId { get; set; }


    }
}
