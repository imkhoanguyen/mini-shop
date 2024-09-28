using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext : IdentityDbContext<AppUser>
    {
        public StoreContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessageSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.MessageReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);


        }

    }
}
