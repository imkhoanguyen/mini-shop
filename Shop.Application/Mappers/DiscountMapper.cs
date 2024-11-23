using Shop.Application.DTOs.Discounts;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class DiscountMapper
    {
        public static Discount DiscountAddDtoToEntity(DiscountAdd discount)
        {
            return new Discount
            {
                Name = discount.Name,
                AmountOff=discount.AmountOff,
                PercentOff=discount.PercentOff,
                PromotionCode=discount.PromotionCode
            };
        }
        public static Discount DiscountUpdateDtoToEntity(DiscountUpdate discount)
        {
            return new Discount
            {
                Id = discount.Id,
                Name = discount.Name,
                AmountOff=discount.AmountOff,
                PercentOff=discount.PercentOff,
                PromotionCode=discount.PromotionCode
            };
        }

        public static DiscountDto EntityToDiscountDto(Discount discount)
        {
            return new DiscountDto
            {
                Id = discount.Id,
                Name = discount.Name,
                AmountOff=discount.AmountOff,
                PercentOff=discount.PercentOff,
                PromotionCode=discount.PromotionCode
            };
        }
    }
}
