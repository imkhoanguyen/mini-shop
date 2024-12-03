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
                new Color { Name = "Red", Code = "#FF0000" }, //1
                new Color { Name = "Green", Code = "#00FF00" },//2
                new Color { Name = "Blue", Code = "#0000FF" },//3
                new Color { Name = "Yellow", Code = "#FFFF00" },//4
                new Color { Name = "Black", Code = "#000000" },//5
                new Color { Name = "White", Code = "#FFFFFF" },//6
                new Color { Name = "Purple", Code = "#800080" },//7
                new Color { Name = "Orange", Code = "#FFA500" },//8
                new Color { Name = "Gray", Code = "#CCCCCC" },//9
                new Color { Name = "Pink", Code = "#FF99FF" },//10
                new Color { Name = "Brown", Code = "#F4A460" },//11

            };

            await context.Colors.AddRangeAsync(colors);
        }
    }
}
