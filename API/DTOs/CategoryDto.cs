using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }

        public static Category toCategory(CategoryDto categoryDto){
            return new Category{
                Name = categoryDto.Name,
                ParentId = categoryDto.ParentId
            };    
        }
    }
    public class CategoryAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
        public static Category toCategory(CategoryAddDto categoryAddDto){
            return new Category{
                Name = categoryAddDto.Name,
                ParentId = categoryAddDto.ParentId
            };    
        }
    }

}