using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class DiscountSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Discounts.Any())
            {
                return;
            }
            var discounts = new List<Discount>
            {
                new Discount { Name = "Summer Sale", AmountOff = 50, PercentOff = null, PromotionCode = "SUMMER50" },
                new Discount { Name = "Holiday Discount", AmountOff = 30, PercentOff = null, PromotionCode = "HOLIDAY30" },
                new Discount { Name = "Flash Sale", AmountOff = null, PercentOff = 10, PromotionCode = "FLASH10" }
            };
            await context.Discounts.AddRangeAsync(discounts);
        }
    }
}
