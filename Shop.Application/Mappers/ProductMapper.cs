using Shop.Application.DTOs.Products;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ProductMapper
    {
        public static Product ProductAddDtoToEntity(ProductAdd productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ProductCategories = productDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Status = productDto.Status,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
        }

        public static Product ProductUpdateDtoToEntity(ProductUpdate productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                ProductCategories = productDto.CategoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList(),
                Status = productDto.Status,
                Updated = DateTime.UtcNow,
            };
        }
        public static ProductDto EntityToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                Status = product.Status,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                Variants = product.Variants.Select(VariantMapper.EntityToVariantDto).ToList()
            };
        }

    }
}
