using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.BookDto
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int PublishedYear { get; set; }
        public string AuthorName { get; set; } = null!;
    }
}
