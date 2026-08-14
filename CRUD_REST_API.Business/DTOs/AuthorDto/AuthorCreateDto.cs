using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.AuthorDto
{
    public class AuthorCreateDto
    {
        public string Name { get; set; } = null!;
        public string Biography { get; set; } = null!;
    }
}
