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

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryIds, 
                    opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.CategoryId).ToList()));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductCategories, 
                    opt => opt.MapFrom(src => src.CategoryIds.Select(catId => new ProductCategory { CategoryId = catId }).ToList()));
        }
    }
}