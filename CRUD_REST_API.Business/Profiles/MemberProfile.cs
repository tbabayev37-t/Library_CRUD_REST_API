using AutoMapper;
using CRUD_REST_API.Business.DTOs.MemberDto;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Profiles
{
    public class MemberProfile:Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberGetDto>().ReverseMap();
            CreateMap<Member, MemberUpdateDto>().ReverseMap();
            CreateMap<Member, MemberCreateDto>().ReverseMap();
        }
    }
}
