using System.ComponentModel.DataAnnotations;

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
    }
}