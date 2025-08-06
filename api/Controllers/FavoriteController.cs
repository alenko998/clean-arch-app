using api.DTOs.Blog;
using api.DTOs.Writer;
using api.Mapping;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _repository;

        public FavoriteController(IFavoriteRepository repository)
        {
            _repository = repository;
        }

        // POST: /api/favorite/{writerId}/favorite/{blogId}
        [HttpPost("{writerId}/favorite/{blogId}")]
        public async Task<IActionResult> AddFavorite(int writerId, int blogId)
        {
            var result = await _repository.AddFavoriteAsync(writerId, blogId);

            if (result.Contains("not found") || result.Contains("Already"))
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: /api/favorite/{writerId}/favorite/{blogId}
        [HttpDelete("{writerId}/favorite/{blogId}")]
        public async Task<IActionResult> RemoveFavorite(int writerId, int blogId)
        {
            var result = await _repository.RemoveFavoriteAsync(writerId, blogId);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }

        // GET: /api/favorite/writer/{writerId}
        [HttpGet("writer/{writerId}")]
        public async Task<IActionResult> GetBlogsByWriter(int writerId)
        {
            try
            {
                var blogs = await _repository.GetBlogsByWriterAsync(writerId);
                var dto = blogs.Select(BlogMapping.ToDto);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: /api/favorite/blog/{blogId}
        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetWritersByBlog(int blogId)
        {
            try
            {
                var writers = await _repository.GetWritersByBlogAsync(blogId);
                var dto = writers.Select(WriterMapping.ToDto);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
