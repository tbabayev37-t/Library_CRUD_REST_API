using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.BookDto
{
    public class BookCreateDto
    {
        public string Title { get; set; } = null!;
       // public string Genre { get; set; } = null!;
        public int PublishedYear { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
