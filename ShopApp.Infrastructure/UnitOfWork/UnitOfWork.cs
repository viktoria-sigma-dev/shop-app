using ShopApp.Domain.Interfaces;
using ShopApp.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace ShopApp.Infrastructure.UnitOfWork
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private IDbContextTransaction? _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
    }
}