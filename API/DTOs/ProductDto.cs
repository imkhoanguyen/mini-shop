using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
         public List<VariantDto> Variants { get; set; } = new List<VariantDto>();

        public static Product toProduct(ProductDto productDto){
            return new Product{
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                ProductCategories = productDto.CategoryIds.Select(c => new ProductCategory{CategoryId = c}).ToList(),
                Variants = productDto.Variants.Select(v => VariantDto.toVariant(v)).ToList()
            };    
        }
    }
    public class ProductAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
         public List<VariantAddDto> Variants { get; set; } = new List<VariantAddDto>();

        public static Product toProduct(ProductAddDto productAddDto){
            return new Product{
                Name = productAddDto.Name,
                Description = productAddDto.Description,
                ProductCategories = productAddDto.CategoryIds.Select(c => new ProductCategory{CategoryId = c}).ToList(),
                Variants = productAddDto.Variants.Select(v => VariantAddDto.toVariant(v)).ToList()
            };    
        }
    }
    
}