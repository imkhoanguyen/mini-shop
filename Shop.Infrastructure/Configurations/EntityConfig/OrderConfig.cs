using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.ShippingFee).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DiscountPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
        }
    }
}
