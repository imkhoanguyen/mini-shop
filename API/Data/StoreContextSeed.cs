﻿using API.Entities;

namespace API.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.AppRoles.Any())
            {
                var roles = new List<AppRole>()
                {
                    new AppRole{Name = "Admin", Description = "Management system"},
                    new AppRole{Name = "Customer", Description = "Shopping"},
                };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
    }
}
