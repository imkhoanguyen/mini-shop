using API.Data;
using API.Entities;

namespace api.Data.Seed
{
    public class CategorySeed
    {
        public static async Task SeedAsync(StoreContext context){
            if(context.Categories.Any()){
                return;
            }
            var categories = new List<Category>{
                new Category { Name = "Tiểu thuyết" },       
                new Category { Name = "Trinh thám" },        
                new Category { Name = "Khoa học viễn tưởng" }, 
                new Category { Name = "Giả tưởng" },                 
                new Category { Name = "Lịch sử" },            
                new Category { Name = "Thơ ca" },
                new Category { Name = "Sách giáo khoa" },            
                new Category { Name = "Sách thiếu nhi" },     
            };
            await context.Categories.AddRangeAsync(categories);
        }
    }

}