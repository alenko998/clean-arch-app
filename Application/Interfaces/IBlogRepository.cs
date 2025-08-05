using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog?> UpdateAsync(int id, Blog blog);
        Task<string?> DeleteAsync(int id);
        Task<Blog?> GetByIdAsync(int id);


    }
}
