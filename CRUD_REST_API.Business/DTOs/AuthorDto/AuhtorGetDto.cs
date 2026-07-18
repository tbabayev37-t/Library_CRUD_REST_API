using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.AuthorDto
{
    public class AuhtorGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public ICollection<BookGetDto> Books { get; set; } = new List<BookGetDto>();
    }
}
