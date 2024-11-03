using Shop.Application.DTOs.Products;
using Shop.Application.DTOs.Variants;
using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Application.Mappers
{
    public class ProductMapper
    {
        public static Product ProductDtoToEntity(ProductDto productDto)
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

        public static Product ProductAddDtoToEntity(ProductAddDto productAddDto)
        {
            return new Product
            {
                Name = productAddDto.Name,
                Description = productAddDto.Description,
                ProductCategories = productAddDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
            };
        }

        public static ProductGetDto toProductGetDto(Product product)
        {
            return new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                Variants = product.Variants.Select(v => new VariantGetDto
                {
                    Id = v.Id,
                    Price = v.Price,
                    PriceSell = v.PriceSell,
                    Quantity = v.Quantity,
                    SizeId = v.SizeId ?? 0,
                    ColorId = v.ColorId ?? 0,
                    //ImageUrls = v.Images.Select(i => new ImageGetDto
                    //{
                    //    Id = i.Id,
                    //    Url = i.Url,
                    //    IsMain = i.IsMain
                    //}).ToList()
                }).ToList(),
                Status = (int)product.Status
            };
        }
    }
}
