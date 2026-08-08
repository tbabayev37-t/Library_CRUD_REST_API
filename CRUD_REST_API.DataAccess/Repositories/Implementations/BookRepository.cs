using CRUD_REST_API.Contexts;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations.Generic;
using CRUD_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_REST_API.DataAccess.Repositories.Implementations
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<(IEnumerable<Book> Books, int TotalCount)> GetAllBooksWithAuthorsAsync(
            int pageNumber,
            int pageSize,
            string? sortBy,
            bool isDescending,
            string? searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            // 1. SearchTerm üzrə filtrləmə 
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(term) ||
                                         (b.Genre != null && b.Genre.ToLower().Contains(term)));
            }

            // 2. Minimum qiymət üzrə filtrləmə
            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price.HasValue && b.Price.Value >= minPrice.Value);
            }

            // 3. Maksimum qiymət üzrə filtrləmə
            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price.HasValue && b.Price.Value <= maxPrice.Value);
            }

            // 4. Sıralama (Sorting)
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

            var totalCount = await query.CountAsync();
            var books = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (books, totalCount);
        }
        public async Task<Book?> GetByIdAsync(int id)
{
    return await _context.Books
        .Include(b => b.Author) // Müəllif məlumatını da qoşuruq
        .FirstOrDefaultAsync(b => b.Id == id);
}
    }
}