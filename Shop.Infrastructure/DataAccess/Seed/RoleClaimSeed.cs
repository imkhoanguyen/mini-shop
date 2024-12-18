﻿using Microsoft.AspNetCore.Identity;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class RoleClaimSeed
    {
        public static async Task SeedAsync(StoreContext context, RoleManager<AppRole> roleManager)
        {
            if (context.RoleClaims.Any()) return;

            var adminRole = await roleManager.FindByNameAsync("Admin");
            var adminClaims = ClaimStore.adminClaims.Select(claim => new IdentityRoleClaim<string>
            {
                RoleId = adminRole.Id,
                ClaimType = claim.ClaimType,
                ClaimValue = claim.ClaimValue,
            }).ToList();

            await context.RoleClaims.AddRangeAsync(adminClaims);

            var customerRole = await roleManager.FindByNameAsync("Customer");
            var customerClaims = new IdentityRoleClaim<string>
            {
                RoleId = customerRole.Id,
                ClaimType = "Permission",
                ClaimValue = ClaimStore.Order_Comfirm
            };

            await context.RoleClaims.AddAsync(customerClaims);

            await context.SaveChangesAsync();
        }
    }
}
