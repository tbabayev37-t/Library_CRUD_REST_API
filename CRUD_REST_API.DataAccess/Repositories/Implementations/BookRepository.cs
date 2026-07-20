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
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync(int pageNumber, int pageSize, string? sortBy, bool isDescending)
        {
            var query = _context.Books.Include(b=>b.Author).AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDescending ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title);
                }
                else if (sortBy.Equals("PublishedYear", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDescending ? query.OrderByDescending(b => b.PublishedYear) : query.OrderBy(b => b.PublishedYear);
                }
            }
            else
            {
                query = query.OrderBy(b => b.Id);
            }
            var books = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return books;
        }
    }
}