using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;

        public ICollection<BookCategory> BookCategories { get; set; }=[];

    }
}
