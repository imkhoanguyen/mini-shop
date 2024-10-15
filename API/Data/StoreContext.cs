﻿using API.Entities;
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
        public DbSet<CartItems>CartItems{get;set;}
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts {get;set;}
        public DbSet<ShippingMethod> ShippingMethods {get;set;}
        public DbSet<Address> Addresses { get; set; }
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

            builder.Entity<CartItems>()
                .HasOne(ci=>ci.ShoppingCart)
                .WithMany(sc =>sc.CartItems)
                .HasForeignKey(ci=>ci.ShoppingCartId);


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
            base.OnModelCreating(builder);


        }

    }
}
