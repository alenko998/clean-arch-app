using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<UserInfo>> GetAllAsync();
        Task<UserInfo?> GetByIdAsync(int id);
        Task<UserInfo> CreateAsync(UserInfo userInfo);
        Task<UserInfo?> UpdateAsync(int id, UserInfo userInfo);
        Task<string?> DeleteAsync(int id);
    }
}
