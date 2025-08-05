using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly AppDbContext _context;

        public UserInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync()
        {
            return await _context.UserInfos.ToListAsync();
        }

        public async Task<UserInfo?> GetByIdAsync(int id)
        {
            return await _context.UserInfos.FindAsync(id);
        }

        public async Task<UserInfo> CreateAsync(UserInfo userInfo)
        {
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();
            return userInfo;
        }

        public async Task<UserInfo?> UpdateAsync(int id, UserInfo userInfo)
        {
            var existing = await _context.UserInfos.FindAsync(id);
            if (existing is null)
                return null;

            existing.Email = userInfo.Email;
            existing.FirstName = userInfo.FirstName;
            existing.LastName = userInfo.LastName;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var existing = await _context.UserInfos.FindAsync(id);
            if (existing is null)
                return null;

            _context.UserInfos.Remove(existing);
            await _context.SaveChangesAsync();
            return $"UserInfo with ID {id} deleted.";
        }
    }
}
