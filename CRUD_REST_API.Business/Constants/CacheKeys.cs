using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Constants
{
    public static class CacheKeys
    {
        public const string AllBooks = "all_books";
        // Dinamik (id ilə olan) keşlər üçün köməkçi metod:
        public static string BookById(int id) => $"book_{id}";
    }
}
