using CRUD_REST_API.Contexts;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations.Generic;
using CRUD_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Repositories.Implementations
{
    public class AuthorRepository:Repository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }
    }
}
