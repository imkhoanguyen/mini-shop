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
           CreateMap<CategoryDto, Category>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
       }
    }
}