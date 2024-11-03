
using API.DTOs;

namespace Shop.Application.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public int Status { get; set; }

        
    }
}