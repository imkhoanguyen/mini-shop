using Microsoft.AspNetCore.Identity;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public class AddressSeed
    {
        public static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager)
        {
            if (context.Addresses.Any())
            {
                return;
            }
            var customerUser = await userManager.FindByNameAsync("Customer");

            var addresses = new List<Address>{
                new Address
                {
                    Street = "517/18 Nguyễn Trãi",
                    District = "Quận 5",
                    City = "Tp.HCM",
                    fullName = "Nguyễn Hữu Việt", 
                    phone = "0456827333", 
                },
                new Address
                {
                    Street = "38 Lý Thái Tổ",
                    District = "Quận 7",
                    City = "Tp.HCM",
                    fullName = "Nguyễn Anh Luân",
                    phone = "075766693",
                }
            };

            customerUser.Address.AddRange(addresses);
            await context.SaveChangesAsync();
        }
    }
}
