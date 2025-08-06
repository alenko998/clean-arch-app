using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddFavoriteAsync(int writerId, int blogId)
        {
            var exists = await _context.Favorites.FindAsync(writerId, blogId);
            if (exists != null)
                return "Already marked as favorite.";

            var writer = await _context.Writers.FindAsync(writerId);
            if (writer == null) return $"Writer {writerId} not found.";

            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return $"Blog {blogId} not found.";

            var favorite = new Favorite
            {
                WriterId = writerId,
                BlogId = blogId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return "Blog favorited.";
        }

        public async Task<string> RemoveFavoriteAsync(int writerId, int blogId)
        {
            var existing = await _context.Favorites.FindAsync(writerId, blogId);
            if (existing == null)
                return "Favorite not found.";

            _context.Favorites.Remove(existing);
            await _context.SaveChangesAsync();

            return "Removed from favorites.";
        }

        public async Task<IEnumerable<Blog>> GetBlogsByWriterAsync(int writerId)
        {
            var exists = await _context.Writers.AnyAsync(w => w.Id == writerId);
            if (!exists)
                throw new KeyNotFoundException($"Writer with ID {writerId} not found.");

            return await _context.Favorites
                .Where(f => f.WriterId == writerId)
                .Include(f => f.Blog)
                .Select(f => f.Blog)
                .ToListAsync();
        }

        public async Task<IEnumerable<Writer>> GetWritersByBlogAsync(int blogId)
        {
            return await _context.Favorites
                .Where(f => f.BlogId == blogId)
                .Include(f => f.Writer)
                    .ThenInclude(w => w.UserInfo)
                .Select(f => f.Writer)
                .ToListAsync();
        }
    }
}
