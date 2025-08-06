using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWriterRepository
    {
        Task<IEnumerable<Writer>> GetAllAsync();
    }
}
