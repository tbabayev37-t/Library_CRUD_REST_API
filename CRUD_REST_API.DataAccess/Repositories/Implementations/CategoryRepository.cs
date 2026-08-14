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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<bool> IsCategoryNameExistAsync(string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
