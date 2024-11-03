using Shop.Application.DTOs.Categories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class CategoryMapper
    {
        public static Category CategoryDtoToEntity(CategoryDto categoryDto)
        {
            return new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
        }

        public static CategoryDto EntityToCategoryDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Created = category.Created,
                Updated = category.Updated,
            };
        }
    }
}
