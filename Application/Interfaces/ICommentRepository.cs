
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetAllByBlogIdAsync(int blogId);
        Task<string?> DeleteAsync(int id);
        Task<Comment?> UpdateAsync(int id, Comment updatedComment);
    }

}
