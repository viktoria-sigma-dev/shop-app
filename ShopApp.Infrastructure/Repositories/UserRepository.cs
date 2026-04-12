using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Interfaces;
using ShopApp.Infrastructure.DBContext;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<List<User>> GetAllAsync()
        {
            return _context.Users.ToListAsync();
        }
        public Task<User?> GetOneAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.UserId == id);
        }

        public Task<User?> GetOneAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}