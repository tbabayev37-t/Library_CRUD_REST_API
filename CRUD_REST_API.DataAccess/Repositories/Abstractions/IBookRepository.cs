using CRUD_REST_API.DataAccess.Repositories.Abstractions.Generic;
using CRUD_REST_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_REST_API.DataAccess.Repositories.Abstractions
{
    public interface IBookRepository : IRepository<Book>
    {

        Task<(IEnumerable<Book> Books, int TotalCount)> GetAllBooksWithAuthorsAsync(
          int pageNumber,
          int pageSize,
          string? sortBy,
          bool isDescending,
          string? searchTerm = null,
          decimal? minPrice = null,
          decimal? maxPrice = null);
    }
}