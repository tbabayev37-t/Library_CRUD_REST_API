using AutoMapper;
using CRUD_REST_API.Business.DTOs.OrderDto;
using CRUD_REST_API.Business.DTOs.OrderItemDto;
using CRUD_REST_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemGetDto>().ReverseMap();

            CreateMap<Order, OrderGetDto>()
                .ForMember(dest=>dest.Items, opt=>opt.MapFrom(src=>src.OrderItems)).ReverseMap();
        }
    }
}
