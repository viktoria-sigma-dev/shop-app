using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(o => o.TransactionId);

            builder.Property(t => t.Status)
                .HasConversion<string>();

            builder.HasOne(o => o.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}