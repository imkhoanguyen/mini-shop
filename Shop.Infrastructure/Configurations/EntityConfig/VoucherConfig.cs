using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class VoucherConfig : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.Property(x => x.percentage).HasColumnType("decimal(18,2)");
        }
    }
}
