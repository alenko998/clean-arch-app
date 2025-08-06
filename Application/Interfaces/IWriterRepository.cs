using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWriterRepository
    {
        Task<IEnumerable<Writer>> GetAllAsync();
        Task<Writer?> GetByIdAsync(int id);
        Task<Writer?> GetByUsernameAsync(string username);
        Task<Writer> CreateAsync(Writer writer);
    }
}
