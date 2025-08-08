using api.DTOs.Blog;
using api.Helpers;
using api.Mapping;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] CreateBlogDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("Title and content are required.");

            var writerId = TokenReader.GetWriterIdFromClaims(User);
            if (writerId <= 0)
                return Unauthorized("WriterId is missing or invalid in token.");

            var blog = BlogMapping.ToEntity(dto, writerId);
            var created = await _blogRepository.CreateAsync(blog);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, BlogMapping.ToDto(created));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid blog ID.");

            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
                return NotFound($"Blog with ID {id} not found.");

            var writerId = TokenReader.GetWriterIdFromClaims(User);
            if (writerId <= 0)
                return Unauthorized("WriterId is missing or invalid in token.");

            if (blog.WriterId != writerId)
                return Forbid("You are not allowed to delete this blog.");

            var result = await _blogRepository.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateBlog(int id, UpdateBlogDto dto)
        {
            if (id <= 0 || dto == null)
                return BadRequest("Invalid blog id or data.");

            var existingBlog = await _blogRepository.GetByIdAsync(id);
            if (existingBlog == null)
                return NotFound($"Blog with ID {id} not found.");

            var writerId = TokenReader.GetWriterIdFromClaims(User);
            if (writerId <= 0)
                return Unauthorized("WriterId is missing or invalid in token.");

            if (existingBlog.WriterId != writerId)
                return Forbid("You are not allowed to update this blog.");

            var blogToUpdate = BlogMapping.ToEntityFromUpdate(dto);
            blogToUpdate.Id = id;
            blogToUpdate.WriterId = existingBlog.WriterId;

            var updated = await _blogRepository.UpdateAsync(id, blogToUpdate);
            return Ok(updated);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);

            if (blog is null)
                return NotFound($"Blog with ID {id} not found.");

            return Ok(BlogMapping.ToDto(blog));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAll()
        {
            var blogs = await _blogRepository.GetAllAsync();
            var result = blogs.Select(BlogMapping.ToDto);
            return Ok(result);
        }
    }
}
