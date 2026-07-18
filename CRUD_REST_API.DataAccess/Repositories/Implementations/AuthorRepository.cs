using CRUD_REST_API.Contexts;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations.Generic;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Repositories.Implementations
{
    public class AuthorRepository:Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
