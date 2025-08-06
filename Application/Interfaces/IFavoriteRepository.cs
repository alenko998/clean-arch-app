using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<string> AddFavoriteAsync(int writerId, int blogId);
        Task<string> RemoveFavoriteAsync(int writerId, int blogId);
        Task<IEnumerable<Blog>> GetBlogsByWriterAsync(int writerId);
        Task<IEnumerable<Writer>> GetWritersByBlogAsync(int blogId);
    }
}
