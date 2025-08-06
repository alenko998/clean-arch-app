using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWriterRepository
    {
        Task<IEnumerable<Writer>> GetAllAsync();
        Task<Writer> CreateAsync(Writer writer);
        Task<Writer?> GetByUsernameAsync(string username);
    }
}
