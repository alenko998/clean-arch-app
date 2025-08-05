using api.DTOs.Comment;

namespace api.DTOs.Blog
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new();
    }
}
