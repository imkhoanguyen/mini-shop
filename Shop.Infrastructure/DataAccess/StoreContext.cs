using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.Configurations.EntityConfig;
using System.Reflection.Emit;

namespace Shop.Infrastructure.DataAccess
{
    public class StoreContext : IdentityDbContext<AppUser>
    {
        public StoreContext(DbContextOptions options) : base(options) { }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Product_Voucher> Product_Vouchers { get; set; }
        public DbSet<Discount> Discounts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        }

    }
}
