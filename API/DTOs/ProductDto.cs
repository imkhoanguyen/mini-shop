using System.ComponentModel.DataAnnotations;
using API.Interfaces;
using API.Entities;
using API.Data.Enum;

namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public int Status { get; set; }

        public static Product toProduct(ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                ProductCategories = productDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Status = (ProductStatus)productDto.Status
            };
        }
    }
    public class ProductAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();

        public static Product toProduct(ProductAddDto productAddDto)
        {
            return new Product
            {
                Name = productAddDto.Name,
                Description = productAddDto.Description,
                ProductCategories = productAddDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
            };
        }
    }
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<VariantGetDto> Variants { get; set; } = new List<VariantGetDto>();
        public int Status { get; set;}
        
    }
}