using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public static Category toCategory(CategoryDto categoryDto){
            return new Category{
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };    
        }
    }
    public class CategoryAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public static Category toCategory(CategoryAddDto categoryAddDto){
            return new Category{
                Name = categoryAddDto.Name,
            };    
        }
    }

}