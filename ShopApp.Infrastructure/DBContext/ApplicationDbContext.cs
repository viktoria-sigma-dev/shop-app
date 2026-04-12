using Microsoft.EntityFrameworkCore;
using ShopApp.Infrastructure.Configurations;

namespace ShopApp.Infrastructure.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Domain.Entities.User> Users { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<Domain.Entities.OrderItem> OrderItems { get; set; }
        public DbSet<Domain.Entities.Product> Products { get; set; }
        public DbSet<Domain.Entities.Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}