using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess.Repositories.Abstractions.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Repositories.Abstractions
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Task<bool> IsCategoryNameExistAsync(string name);
    }
}
