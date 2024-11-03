using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class DiscountConfig : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(x => x.AmountOff).HasColumnType("decimal(18,2)");
            builder.Property(x => x.PercentOff).HasColumnType("decimal(18,2)");
        }
    }
}
