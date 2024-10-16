using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data.Seed
{
    public class ProductSeed
    {
        public static async Task SeedAsync(StoreContext context){
            if(context.Products.Any()){
                return;
            }
            var products = new List<Product>{
                new Product { Name = "Ao", Description = "Ao thun"},       
                     
            };
            await context.Products.AddRangeAsync(products);
        }
    }
}