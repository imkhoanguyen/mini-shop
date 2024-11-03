namespace Shop.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public required string Name { get; set; }
        public decimal? AmountOff { get; set; }
        public decimal? PercentOff { get; set; }
        public required string PromotionCode { get; set; }
        public bool IsDelete { get; set; }
    }
}
