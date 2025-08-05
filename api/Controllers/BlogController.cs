using api.DTOs.Blog;
using api.Mapping;
using Application.Interfaces;
using Domain.Entities;
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
        public async Task<IActionResult> Create([FromBody] CreateBlogDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Content))
            {
                return BadRequest("Title and content are required.");
            }
            var blog = BlogMapping.ToEntity(dto);
            var created = await _blogRepository.CreateAsync(blog);

            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid blog ID.");
            var result = await _blogRepository.DeleteAsync(id);
            if (result == null)
                return NotFound($"Blog with ID {id} not found.");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);

            if (blog is null)
                return NotFound($"Blog with ID {id} not found.");

            return Ok(BlogMapping.ToDto(blog));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, UpdateBlogDto dto)
        {
            if (id <= 0 || dto == null)
                return BadRequest("Invalid blog id or data.");

            var blogToUpdate = BlogMapping.ToEntityFromUpdate(dto);
            blogToUpdate.Id = id; // osiguraj da id dolazi iz route

            var updated = await _blogRepository.UpdateAsync(id, blogToUpdate);

            if (updated == null)
                return NotFound($"Blog with ID {id} not found.");

            return Ok(updated);
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
