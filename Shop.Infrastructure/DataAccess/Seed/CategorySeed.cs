using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class CategorySeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }
            var categories = new List<Category>{
                new Category { Name = "T-Shirt" },
                new Category { Name = "Jacket" },
                new Category { Name = "Sweater" },
                new Category { Name = "Blouse" },
                new Category { Name = "Suits & Blazers" },
                new Category { Name = "Outerwear" },
                new Category { Name = "Cardigans" },
            };
            await context.Categories.AddRangeAsync(categories);
        }
    }

}