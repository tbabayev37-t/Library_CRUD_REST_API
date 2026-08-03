using AutoMapper;
using CRUD_REST_API.Business.DTOs.CategoryDto;
using CRUD_REST_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();
        }
    }
}
