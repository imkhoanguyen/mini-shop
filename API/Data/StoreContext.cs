using System.Security.Cryptography;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace API.Data
{
    public class StoreContext : IdentityDbContext<AppUser>
    {
        public StoreContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItems>CartItems{get;set;}
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts {get;set;}
        public DbSet<ShippingMethod> ShippingMethods {get;set;}
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payments> Payments{get;set;}
        public DbSet<Order> Orders{get;set;}
        public DbSet<OrderItems> OrderItems{get;set;}

        public DbSet<Voucher> Vouchers{ get; set; }
        public DbSet<Product_Voucher> Product_Vouchers{ get; set; }


        public DbSet<Voucher> Vouchers{ get; set; }
        public DbSet<Product_Voucher> Product_Vouchers{ get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<ShippingMethod> ShippingMethods{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItems> OrderItems{ get; set; }
        //public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            builder.Entity<OrderItems>()
                .HasKey(oi => new{ oi.ProductId, oi.OrderId});

            
            
            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            builder.Entity<CartItems>()
                .HasOne(ci=>ci.ShoppingCart)
                .WithMany(sc =>sc.CartItems)
                .HasForeignKey(ci=>ci.ShoppingCartId);
            builder.Entity<OrderItems>()
                .HasOne(oi=>oi.Product)
                .WithMany(p=>p.OrderItems)
                .HasForeignKey(p=>p.ProductId);

            builder.Entity<OrderItems>()
                .HasOne(oi=>oi.Order)
                .WithMany(o=>o.OrderItems)
                .HasForeignKey(o=>o.OrderId);
            
            
            // builder.Entity<CartItems>()
            //     .HasOne(ci=>ci.Variants)
            //     .WithMany(p=>p.CartItems)

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

            builder.Entity<Variant>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Variant)
                .HasForeignKey(i => i.VariantId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            // Self-referencing relationship for replies in Review
            builder.Entity<Review>()
                .HasOne(r => r.ParentReview)
                .WithMany(r => r.Replies)
                .HasForeignKey(r => r.ParentReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
            

            builder.Entity<Product_Voucher>()
                .HasKey(pc => new { pc.ProductId, pc.VoucherId });

            builder.Entity<Product_Voucher>()
                .HasKey(pc => new { pc.ProductId, pc.VoucherId });
        }

    }
}
