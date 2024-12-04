using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class ProductUserLikeConfig : IEntityTypeConfiguration<ProductUserLike>
    {
        public void Configure(EntityTypeBuilder<ProductUserLike> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.AppUserId });

            builder
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductUserLikes)
                .HasForeignKey(pc => pc.ProductId);
            builder
                .HasOne(pc => pc.AppUser)
                .WithMany(c => c.ProductUserLikes)
                .HasForeignKey(pc => pc.AppUserId);

        }
    }
}
