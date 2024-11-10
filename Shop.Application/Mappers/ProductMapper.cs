using Shop.Application.DTOs.Products;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ProductMapper
    {
        public static Product ProductAddDtoToEntity(ProductAdd productDto)
        {
            var product = BaseProductDtoToEntity(productDto);
            product.Created = DateTime.UtcNow.AddHours(7);
            product.Updated = DateTime.UtcNow.AddHours(7);
            return product;
        }

        public static Product ProductUpdateDtoToEntity(ProductUpdate productDto)
        {
            var product = BaseProductDtoToEntity(productDto);
            product.Id = productDto.Id;
            product.Updated = DateTime.UtcNow.AddHours(7);
            return product;
        }

        private static Product BaseProductDtoToEntity(ProductBase productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Status = productDto.Status,
                ProductCategories = MapProductCategories(productDto.CategoryIds)
            };
        }

        private static List<ProductCategory> MapProductCategories(List<int> categoryIds)
        {
            return categoryIds.Select(c => new ProductCategory { CategoryId = c }).ToList();
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
                Image = ProductImageToImageProductDto(product.Image),
                Variants = product.Variants.Select(VariantMapper.EntityToVariantDto).ToList()
            };
        }

        public static ImageProductDto ProductImageToImageProductDto(ProductImage entity)
        {
            return new ImageProductDto
            {
                Id = entity?.Id ?? 0,
                ImgUrl = entity?.ImgUrl ?? string.Empty
            };
        }
    }
}
