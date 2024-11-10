using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
               .HasMany(p => p.Variants)
               .WithOne(v => v.Product)
               .HasForeignKey(v => v.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Status).HasConversion(
                o => o.ToString(),
                o => (ProductStatus)Enum.Parse(typeof(ProductStatus), o)
            );
        }
    }
}
