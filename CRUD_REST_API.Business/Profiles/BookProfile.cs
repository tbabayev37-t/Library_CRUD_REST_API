using AutoMapper;
using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Profiles
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookCreateDto>().ReverseMap();            
            CreateMap<Book, BookUpdateDto>().ReverseMap();
            CreateMap<Book, BookGetDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)).ReverseMap();
        }
    }
}
