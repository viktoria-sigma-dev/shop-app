using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersApp.src.Infrastructure.Configurations;

namespace UsersApp.src.Infrastructure.DBContext
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