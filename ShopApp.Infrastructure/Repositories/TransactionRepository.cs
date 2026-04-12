using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Interfaces;
using ShopApp.Infrastructure.DBContext;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Enums;

namespace ShopApp.Infrastructure.Repositories
{
    public class TransactionRepository(ApplicationDbContext context) : ITransactionRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<List<Transaction>> GetAllAsync(int orderId)
        {
            return _context.Transactions.Where(transaction => transaction.OrderId == orderId).ToListAsync();
        }
        public Task<List<Transaction>> GetAllPaidAsync()
        {
            return _context.Transactions.Where(transaction => transaction.Status == TransactionStatus.Paid).Include(t => t.Order).ToListAsync();
        }

        public Task<List<Transaction>> GetAllRefundedAsync()
        {
            return _context.Transactions.Where(transaction => transaction.Status == TransactionStatus.Refunded).Include(t => t.Order).ToListAsync();
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}