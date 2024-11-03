using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class ProductSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Products.Any())
            {
                return;
            }
            var products = new List<Product>{
                new Product { Name = "Ao", Description = "Ao thun"},

            };
            await context.Products.AddRangeAsync(products);
        }
    }
}