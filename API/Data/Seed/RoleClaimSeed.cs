using API.Constains;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data.Seed
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

            await context.SaveChangesAsync();
        }
    }
}
