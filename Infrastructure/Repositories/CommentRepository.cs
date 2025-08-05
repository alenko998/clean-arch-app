using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAllByBlogIdAsync(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogId == blogId)
                .ToListAsync();
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment is null) return null;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return $"Comment with ID {id} deleted.";
        }

        public async Task<Comment?> UpdateAsync(int id, Comment updatedComment)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment is null) return null;

            comment.Content = updatedComment.Content;
            await _context.SaveChangesAsync();
            return comment;
        }
    }

}
