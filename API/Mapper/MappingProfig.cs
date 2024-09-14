using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Mapper
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
       {
           CreateMap<AppUser, UserDto>();
           CreateMap<RegisterDto, AppUser>();
           CreateMap<CategoryDto, Category>();
       }
    }
}