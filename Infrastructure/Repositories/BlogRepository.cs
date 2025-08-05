using Application.Interfaces;        // ← za IBlogRepository
using Domain.Entities;              // ← za Blog
using Infrastructure.Data;         // ← za AppDbContext
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog is null)
                return null;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return $"Blog with ID {id} deleted.";

        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task<Blog?> UpdateAsync(int id, Blog blog)
        {
            var existing = await _context.Blogs.FindAsync(id);
            if (existing is null)
                return null;
            existing.Title = blog.Title;
            existing.Content = blog.Content;
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return blog;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs
                .Include(b => b.Comments)
                .ToListAsync();
        }
    }
}
