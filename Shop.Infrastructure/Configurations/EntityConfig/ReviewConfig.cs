using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Configurations.EntityConfig
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Self-referencing relationship for replies in Review
            builder
                .HasOne(r => r.ParentReview)
                .WithMany(r => r.Replies)
                .HasForeignKey(r => r.ParentReviewId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
