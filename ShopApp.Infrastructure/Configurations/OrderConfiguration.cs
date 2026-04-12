using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.Status)
                .HasConversion<string>();

            builder.HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Transactions)
                .WithOne(o => o.Order)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}