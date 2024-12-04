using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class ShippingMethodSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.ShippingMethods.Any())
            {
                return;
            }
            var shippingMethods = new List<ShippingMethod>
            {
                new ShippingMethod 
                { 
                    Name = "Standard Shipping", 
                    Cost = 5, 
                    EstimatedDeliveryTime = "3-5 business days", 
                    Created = DateTime.UtcNow, 
                    Updated = DateTime.UtcNow, 
                },
                new ShippingMethod 
                { 
                    Name = "Express Shipping", 
                    Cost = 15, 
                    EstimatedDeliveryTime = "1-2 business days", 
                    Created = DateTime.UtcNow, 
                    Updated = DateTime.UtcNow, 
                }
            };
            await context.ShippingMethods.AddRangeAsync(shippingMethods);
            await context.SaveChangesAsync();
        }
    }
}
