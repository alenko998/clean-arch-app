using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Mapping;
using api.DTOs.Writer;
using api.DTOs.Blog;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{writerId}/favorite/{blogId}")]
        public async Task<IActionResult> AddFavorite(int writerId, int blogId)
        {
            var exists = await _context.Favorites.FindAsync(writerId, blogId);
            if (exists != null)
                return BadRequest("Already marked as favorite.");

            var writer = await _context.Writers.FindAsync(writerId);
            if (writer == null) return NotFound($"Writer {writerId} not found.");

            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return NotFound($"Blog {blogId} not found.");

            var favorite = new Favorite
            {
                WriterId = writerId,
                BlogId = blogId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok("Blog favorited.");
        }

        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<IEnumerable<WriterDto>>> GetWritersByBlog(int blogId)
        {
            var writers = await _context.Favorites
                .Where(f => f.BlogId == blogId)
                .Include(f => f.Writer)
                    .ThenInclude(w => w.UserInfo)
                .Select(f => WriterMapping.ToDto(f.Writer))
                .ToListAsync();

            return Ok(writers);
        }

        [HttpGet("writer/{writerId}")]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogsByWriter(int writerId)
        {
            var writerExists = await _context.Writers.AnyAsync(w => w.Id == writerId);
            if (!writerExists)
                return NotFound($"Writer with ID {writerId} not found.");

            var blogs = await _context.Favorites
                .Where(f => f.WriterId == writerId)
                .Include(f => f.Blog)
                .Select(f => BlogMapping.ToDto(f.Blog))
                .ToListAsync();

            return Ok(blogs);
        }
    }
}
