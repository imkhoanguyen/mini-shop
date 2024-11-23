namespace Shop.Application.DTOs.Discounts
{
    public class DiscountBase
    {
        public required string Name { get; set; }
         public decimal? AmountOff { get; set; }
        public decimal? PercentOff { get; set; }
        public required string PromotionCode { get; set; }
    }
}