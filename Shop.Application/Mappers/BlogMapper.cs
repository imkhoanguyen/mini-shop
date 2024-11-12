using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.DTOs.Blog;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class BlogMapper
    {
        public static Blog BlogAddDtoToEntity(BlogAddDto dto)
        {
            return new Blog
            {
                Title = dto.Title,
                Content = dto.Content,
                Category = dto.Category,
                UserId = dto.UserId,
            };
        }

        public static Blog BlogDtoToEntity(BlogDto dto)
        {
            return new Blog
            {
                Id = dto.Id,
                Title = dto.Title,
                Content = dto.Content,
                Category = dto.Category,
                UserId = dto.UserId,
            };
        }

        public static BlogDto EntityToBlogDto(Blog blog)
        {
            return new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Category = blog.Category,
                UserId = blog.UserId,
            };
        }
    }
}