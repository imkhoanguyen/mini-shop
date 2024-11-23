using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class ColorSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Colors.Any())
            {
                return;
            }
            var colors = new List<Color>
            {
                new Color { Name = "Red", Code = "#FF0000" },
                new Color { Name = "Green", Code = "#00FF00" },
                new Color { Name = "Blue", Code = "#0000FF" },
                new Color { Name = "Yellow", Code = "#FFFF00" },
                new Color { Name = "Black", Code = "#000000" },
                new Color { Name = "White", Code = "#FFFFFF" },
                new Color { Name = "Purple", Code = "#800080" },
                new Color { Name = "Orange", Code = "#FFA500" }
            };

            await context.Colors.AddRangeAsync(colors);
        }
    }
}
