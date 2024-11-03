using Shop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Categories
{
    public class CategoryAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public static Category toCategory(CategoryAddDto categoryAddDto)
        {
            return new Category
            {
                Name = categoryAddDto.Name,
            };
        }
    }
}
