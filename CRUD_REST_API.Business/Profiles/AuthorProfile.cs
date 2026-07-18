using AutoMapper;
using CRUD_REST_API.Business.DTOs.AuthorDto;
using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Profiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorCreateDto>().ReverseMap();
            CreateMap<Author, AuthorUpdateDto>().ReverseMap();
            CreateMap<Author, AuhtorGetDto>().ReverseMap();
        }
    }
}
