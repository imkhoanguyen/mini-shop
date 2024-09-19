using System.ComponentModel.DataAnnotations;
using API.Interfaces;
using API.Entities;

namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public int VariantId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public List<ImageDto>? ImageUrls { get; set; } = new List<ImageDto>();
        public List<int> ImageId { get; set; } = new List<int>();
        public IFormFileCollection? Images { get; set; }


        public static async Task<Product> toProduct(ProductDto productDto, IImageService imageService)
        {
            var images = new List<Image>();
            if (productDto.Images != null)
            {
                foreach (var file in productDto.Images)
                {
                    var uploadResult = await imageService.UploadImageAsync(file);
                    images.Add(new Image
                    {
                        Id = productDto.ImageId.FirstOrDefault(),
                        Url = uploadResult.Url.ToString(),
                        PublicId = uploadResult.PublicId
                    });
                }
            }
            var variants = new Variant
            {
                Id = productDto.VariantId,
                Price = productDto.Price,
                PriceSell = productDto.PriceSell,
                Quantity = productDto.Quantity,
                SizeId = productDto.SizeId,
                ColorId = productDto.ColorId
            };
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                ProductCategories = productDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Variants = new List<Variant> { variants },
                Images = images
            };
        }
    }
    public class ProductAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        //Variant
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        //Image
        public IFormFileCollection? Images { get; set; }


        public static async Task<Product> toProduct(ProductAddDto productAddDto, IImageService imageService)
        {
            var imageUrls = new List<Image>();
            if (productAddDto.Images != null)
            {
                foreach (var file in productAddDto.Images)
                {
                    var uploadResult = await imageService.UploadImageAsync(file);
                    imageUrls.Add(new Image
                    {
                        Url = uploadResult.Url.ToString(),
                        PublicId = uploadResult.PublicId
                    });
                }
            }
            var variants = new Variant
            {
                Price = productAddDto.Price,
                PriceSell = productAddDto.PriceSell,
                Quantity = productAddDto.Quantity,
                SizeId = productAddDto.SizeId,
                ColorId = productAddDto.ColorId
            };

            return new Product
            {
                Name = productAddDto.Name,
                Description = productAddDto.Description,
                ProductCategories = productAddDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Variants = new List<Variant> { variants },
                Images = imageUrls
            };

        }

    }

}