using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class ProductVoucherConfig : IEntityTypeConfiguration<Product_Voucher>
    {
        public void Configure(EntityTypeBuilder<Product_Voucher> builder)
        {
            builder
                .HasKey(pc => new { pc.ProductId, pc.VoucherId });
        }
    }
}
