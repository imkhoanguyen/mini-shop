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
                new Size { Name = "S" }, //1
                new Size { Name = "M" }, //2
                new Size { Name = "L" },//3
                new Size { Name = "XL" },//4
                new Size { Name = "XXL" }//5
            };

            await context.Sizes.AddRangeAsync(sizes);
        }
    }
}
