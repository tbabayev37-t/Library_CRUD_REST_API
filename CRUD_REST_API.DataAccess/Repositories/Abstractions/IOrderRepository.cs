using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess.Repositories.Abstractions.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Repositories.Abstractions
{
    public interface IOrderRepository:IRepository<Order>
    {
        Task<Order?> GetOrderWithDetailsAsync(int id);
        Task<IEnumerable<Order>> GetAllWithDetailsAsync();
    }
}
