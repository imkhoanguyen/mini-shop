using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Products
{
    public class ProductUpdate : ProductBase
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
