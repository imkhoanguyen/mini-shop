using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class ShippingMethodConfig : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
