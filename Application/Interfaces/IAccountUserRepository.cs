using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountUserRepository
    {
        Task<IEnumerable<AccountUser>> GetAllAsync();
        Task<AccountUser?> GetByIdAsync(int id);
        Task<AccountUser> CreateAsync(AccountUser user);
        Task<AccountUser?> UpdateAsync(int id, AccountUser user);
        Task<string?> DeleteAsync(int id);
    }
}
