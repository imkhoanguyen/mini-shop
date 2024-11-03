using Shop.Application.DTOs.Variants;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class VariantMapper
    {
        public static Variant VariantDtoToEntity(VariantDto variantDto)
        {
            return new Variant
            {
                Id = variantDto.Id,
                Price = variantDto.Price,
                PriceSell = variantDto.PriceSell,
                Quantity = variantDto.Quantity,
                SizeId = variantDto.SizeId,
                ColorId = variantDto.ColorId
            };
        }

        public static Variant VariantAddDtoToEntity(VariantAddDto variantAddDto)
        {
            var variant = new Variant
            {
                ProductId = variantAddDto.ProductId,
                Price = variantAddDto.Price,
                PriceSell = variantAddDto.PriceSell,
                Quantity = variantAddDto.Quantity,
                SizeId = variantAddDto.SizeId,
                ColorId = variantAddDto.ColorId
            };
            return variant;
        }

        public static VariantGetDto EntityToVariantGetDto(Variant variant)
        {
            return new VariantGetDto
            {
                Id = variant.Id,
                Price = variant.Price,
                PriceSell = variant.PriceSell,
                Quantity = variant.Quantity,
                SizeId = variant.SizeId,
                ColorId = variant.ColorId,
                //ImageUrls = variant.Images.Select(i => new ImageGetDto
                //{
                //    Id = i.Id,
                //    Url = i.Url,
                //    IsMain = i.IsMain
                //}).ToList()
            };
        }
    }
}
