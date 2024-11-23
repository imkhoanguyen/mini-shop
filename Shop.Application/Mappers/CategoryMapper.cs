using Shop.Application.DTOs.Categories;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class CategoryMapper
    {
        //Add Category
        public static Category CategoryAddDtoToEntity(CategoryAdd category)
        {
            return new Category
            {
                Name = category.Name,
                Created = DateTime.UtcNow.AddHours(7),
                Updated = DateTime.UtcNow.AddHours(7),
            };
        }

        //Update Category   
        public static Category CategoryUpdateDtoToEntity(CategoryUpdate category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Updated = DateTime.UtcNow.AddHours(7),
            };
        }

        //Get Category
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
