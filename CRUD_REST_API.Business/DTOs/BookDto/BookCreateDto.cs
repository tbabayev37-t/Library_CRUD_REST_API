using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.BookDto
{
    public class BookCreateDto
    {
        [Required(ErrorMessage ="Basliq yazilmalidir!")]
        [StringLength(100,ErrorMessage ="Kitab adi cox uzundur!")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage ="Janr mutleq qeyd edilmelidir!")]
        public string Genre { get; set; } = null!;
        [Range(1,2026, ErrorMessage ="Nesr ili duzgun qeyd edilmeyib!")]
        public int PublishedYear { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
