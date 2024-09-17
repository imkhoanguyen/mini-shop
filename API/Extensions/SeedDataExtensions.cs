using api.Data;
using api.Data.Seed;
using API.Data;

namespace API.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedDataServices(this IServiceCollection services){
            using(var service = services.BuildServiceProvider()){
                using(var scope = service.CreateScope()){
                var context = service.GetRequiredService<StoreContext>();
                context.Database.EnsureCreated();
                CategorySeed.Seed(context);
                
                }
            }
        }
    }
}