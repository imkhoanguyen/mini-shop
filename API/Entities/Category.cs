using System.ComponentModel.DataAnnotations;
using API.DTOs;

namespace API.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public bool IsDelete {get ; set; } = false;
        public ICollection<ProductCategory>? ProductCategories  { get; set; } = new List<ProductCategory>();
    
        public static CategoryDto toCategoryDto(Category category){
            return new CategoryDto{
                Id = category.Id,
                Name = category.Name,
                Created = category.Created,
                Updated = category.Updated,
            };    
        }
    }

   
}