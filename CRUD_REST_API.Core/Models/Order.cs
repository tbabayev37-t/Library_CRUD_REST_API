using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];   

    }
}
