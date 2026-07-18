using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.AuthorDto
{
    public class AuthorCreateDto
    {
        [Required(ErrorMessage ="Muellif adi bos ola bilmez!")]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(1000, ErrorMessage ="Bioqrafiya 1000 simvoldan cox ola bilmez!")]
        public string Biography { get; set; } = null!;
    }
}
