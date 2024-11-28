using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasIndex(a => a.AppUserId).IsUnique(false);  // Cho phép nhiều địa chỉ với cùng một AppUserId

            // Định nghĩa quan hệ giữa Address và AppUser
            builder.HasOne(a => a.AppUser)  // Một địa chỉ thuộc về một người dùng
                .WithMany(u => u.Address)   // Một người dùng có thể có nhiều địa chỉ
                .HasForeignKey(a => a.AppUserId) // Khóa ngoại liên kết với AppUser
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
