using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class VariantConfig : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            builder
                .HasMany(v => v.Images)
                .WithOne(i => i.Variant)
                .HasForeignKey(i => i.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Status).HasConversion(
                o => o.ToString(),
                o => (VariantStatus)Enum.Parse(typeof(VariantStatus), o));

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.PriceSell).HasColumnType("decimal(18,2)");


        }
    }
}
