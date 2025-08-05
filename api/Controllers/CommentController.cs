using api.DTOs.Comment;
using api.Mapping;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost("{blogId}")]
        public async Task<IActionResult> Create([FromRoute] int blogId, [FromBody] CreateCommentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("Content is required");

            var comment = CommentMapping.ToEntity(dto, blogId);
            var created = await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { blogId = blogId, id = created.Id }, created);
        }

        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetAllByBlogId([FromRoute] int blogId)
        {
            var comments = await _commentRepository.GetAllByBlogIdAsync(blogId);
            return Ok(comments);
        }

        [HttpPut("{blogId}/comments/{commentId}")]
        public async Task<IActionResult> Update([FromRoute] int commentId, [FromBody] UpdateCommentDto dto)
        {
            var updated = await _commentRepository.UpdateAsync(commentId, CommentMapping.ToEntityFromUpdate(dto));
            if (updated is null)
                return NotFound($"Comment with ID {commentId} not found.");

            return Ok(updated);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            var result = await _commentRepository.DeleteAsync(commentId);
            if (result is null)
                return NotFound($"Comment with ID {commentId} not found.");

            return Ok(result);
        }

        [HttpGet("/comments/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid comment ID.");

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found.");

            return Ok(comment);
        }
    }

}
