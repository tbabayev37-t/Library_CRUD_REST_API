using CRUD_REST_API.DataAccess.Repositories.Abstractions.Generic;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Repositories.Abstractions
{
    public interface IAuthorRepository:IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync();
    }
}
