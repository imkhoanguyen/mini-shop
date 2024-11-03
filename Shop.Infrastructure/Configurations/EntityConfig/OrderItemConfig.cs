using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasKey(x=> x.Id);
        }
    }
}
