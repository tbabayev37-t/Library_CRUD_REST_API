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
            CreateMap<BookCreateDto, Book>()
                .ForMember(dest => dest.BookCategories, opt =>
                    opt.MapFrom(src => src.CategoryIds.Select(id => new BookCategory { CategoryId = id })));

            // BookUpdateDto -> Book
            CreateMap<BookUpdateDto, Book>()
                .ForMember(dest => dest.BookCategories, opt =>
                    opt.MapFrom(src => src.CategoryIds.Select(id => new BookCategory { CategoryId = id })));

            // Book -> BookGetDto
            CreateMap<Book, BookGetDto>()
                .ForMember(dest => dest.AuthorName, opt =>
                    opt.MapFrom(src => src.Author != null ? src.Author.Name : string.Empty))
                .ForMember(dest => dest.CategoryNames, opt =>
                    opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category.Name)));
        }
    }
}
