using Shop.Application.DTOs.Variants;
using Shop.Domain.Entities;
using System.Drawing;

namespace Shop.Application.Mappers
{
    public class VariantMapper
    {
        public static Variant VariantAddDtoToEntity(VariantAdd variantDto)
        {
            return new Variant
            {
                ProductId = variantDto.ProductId,
                Price = variantDto.Price,
                PriceSell = variantDto.PriceSell,
                Quantity = variantDto.Quantity,
                SizeId = variantDto.SizeId,
                ColorId = variantDto.ColorId,
                Status = variantDto.Status,
            };
        }

        public static Variant VariantUpdateDtoToEntity(VariantUpdate variantDto)
        {
            return new Variant
            {
                Id = variantDto.Id,
                ProductId = variantDto.ProductId,
                Price = variantDto.Price,
                PriceSell = variantDto.PriceSell,
                Quantity = variantDto.Quantity,
                SizeId = variantDto.SizeId,
                ColorId = variantDto.ColorId,
                Status = variantDto.Status,

            };
        }

        public static VariantDto EntityToVariantDto(Variant variant)
        {
            return new VariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                Price = variant.Price,
                PriceSell = variant.PriceSell,
                Quantity = variant.Quantity,
                Size = SizeMapper.EntityToSizeDto(variant.Size!),
                Color = ColorMapper.EntityToColorDto(variant.Color!),
                Status = variant.Status,
                Images = variant.Images.Select(VariantImageToImageVariantDto).ToList()
            };
        }
        public static ImageVariantDto VariantImageToImageVariantDto(VariantImage entity)
        {
            return new ImageVariantDto
            {
                Id = entity.Id,
                ImgUrl = entity.ImgUrl ?? string.Empty
            };
        }
    }
}
