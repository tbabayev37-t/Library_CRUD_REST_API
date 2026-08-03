using CRUD_REST_API.Contexts;
using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations.Generic;
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
    }
}
