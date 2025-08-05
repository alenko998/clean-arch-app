using api.DTOs.Comment;
using Domain.Entities;

namespace api.Mapping
{
    public static class CommentMapping
    {
        public static Comment ToEntity(CreateCommentDto dto, int blogId)
        {
            return new Comment
            {
                Content = dto.Content,
                BlogId = blogId
            };
        }

        public static Comment ToEntityFromUpdate(UpdateCommentDto dto)
        {
            return new Comment
            {
                Content = dto.Content
            };
        }

        public static CommentDto ToDto(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content
            };
        }
    }
}
