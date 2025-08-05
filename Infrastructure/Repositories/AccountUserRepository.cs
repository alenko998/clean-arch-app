using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountUserRepository : IAccountUserRepository
    {
        private readonly AppDbContext _context;

        public AccountUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountUser>> GetAllAsync()
        {
            return await _context.AccountUsers
                .Include(a => a.UserInfo)
                .ToListAsync();
        }

        public async Task<AccountUser?> GetByIdAsync(int id)
        {
            return await _context.AccountUsers
                .Include(a => a.UserInfo)
                .FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<AccountUser> CreateAsync(AccountUser user)
        {
            _context.AccountUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AccountUser?> UpdateAsync(int id, AccountUser user)
        {
            var existing = await _context.AccountUsers.FindAsync(id);
            if (existing is null)
                return null;

            existing.Username = user.Username;
            existing.Password = user.Password;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var existing = await _context.AccountUsers.FindAsync(id);
            if (existing is null)
                return null;

            _context.AccountUsers.Remove(existing);
            await _context.SaveChangesAsync();
            return $"AccountUser with ID {id} deleted.";
        }
    }
}
