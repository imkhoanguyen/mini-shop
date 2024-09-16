using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }

        public static Category toCategory(CategoryDto categoryDto){
            return new Category{
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                ParentId = categoryDto.ParentId
            };    
        }
    }

}