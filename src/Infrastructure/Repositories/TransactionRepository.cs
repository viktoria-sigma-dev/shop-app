using Microsoft.EntityFrameworkCore;
using UsersApp.src.Domain.Interfaces;
using UsersApp.src.Infrastructure.DBContext;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;

namespace UsersApp.src.Infrastructure.Repositories
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

        public async Task<Transaction?> GetPaidByOrderIdAsync(int orderId)
        {
            return await _context.Transactions.Where(t => t.OrderId == orderId).Where(transaction => transaction.Status == TransactionStatus.Paid).FirstOrDefaultAsync();
        }
        public async Task<Transaction> Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}