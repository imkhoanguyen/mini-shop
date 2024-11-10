using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Products
{
    public class ProductAdd : ProductBase
    {
        public required IFormFile ImageFile { get; set; } 
    }
}
