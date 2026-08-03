using CRUD_REST_API.Contexts;
using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Repositories.Implementations
{
    public class OrderRepository:Repository<Order>,IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderRepository(AppDbContext context):base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Order>> GetAllWithDetailsAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .ThenInclude(x => x.Book).ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int id)
        {
            return await _context.Orders.Include(o=>o.OrderItems)
                .ThenInclude(oi=>oi.Book).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
