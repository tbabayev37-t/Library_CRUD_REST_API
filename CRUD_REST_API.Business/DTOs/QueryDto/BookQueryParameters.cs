using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.QueryDto
{
    public class BookQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public string? SortBy { get; set; } // "Title", "PublishedYear" ve s.
        public bool IsDescending { get; set; } = false; // true olarsa Z-A ve ya boyukden kiciye

        //Checpoint 3: Murekkeb Filt Parametrleri
        public string? SearchTerm { get; set; }  // Title ve ya Genre uzre axtaris
        public decimal? MinPrice { get; set; }   // Minimum qiymet
        public decimal? MaxPrice { get; set; }   // Maksimum qiymet
    }
}
