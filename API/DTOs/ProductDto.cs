using System.ComponentModel.DataAnnotations;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<VariantDto> Variants { get; set; } = new List<VariantDto>();
        public IFormFileCollection Images { get; set; }

        public static async Task<Product> toProduct(ProductDto productDto, IImageService imageService)
        {
            var imageUrls = new List<Image>();
            foreach (var file in productDto.Images)
            {
                var uploadResult = await imageService.UploadImageAsync(file);
                imageUrls.Add(new Image
                {
                    Url = uploadResult.Url.ToString(),
                    PublicId = uploadResult.PublicId
                });
            }
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                ProductCategories = productDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Variants = productDto.Variants.Select(v => VariantDto.toVariant(v)).ToList(),
                Images = imageUrls
            };
        }
    }
    public class ProductAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        [FromForm]
        public List<VariantAddDto> Variants { get; set; } = new List<VariantAddDto>();
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

            var product = new Product
            {
                Name = productAddDto.Name,
                Description = productAddDto.Description,
                ProductCategories = productAddDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Variants = productAddDto.Variants.Select(v => VariantAddDto.toVariant(v)).ToList(),
                Images = imageUrls
            };

            return product;
        }

    }

}