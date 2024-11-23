using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class SizeSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Sizes.Any())
            {
                return;
            }
            var sizes = new List<Size>
            {
                new Size { Name = "S" },
                new Size { Name = "M" },
                new Size { Name = "L" },
                new Size { Name = "XL" },
                new Size { Name = "XXL" }
            };

            await context.Sizes.AddRangeAsync(sizes);
        }
    }
}
