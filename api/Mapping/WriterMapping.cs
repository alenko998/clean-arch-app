using Domain.Entities;
using api.DTOs.Writer;
using api.DTOs.UserInfo;
using api.DTOs.Blog;
using api.DTOs.Comment;

namespace api.Mapping
{
    public static class WriterMapping
    {
        public static WriterDto ToDto(Writer writer)
        {
            return new WriterDto
            {
                Id = writer.Id,
                Username = writer.Username,
                UserInfo = writer.UserInfo is null ? null : new UserInfoDto
                {
                    Email = writer.UserInfo.Email,
                    FirstName = writer.UserInfo.FirstName,
                    LastName = writer.UserInfo.LastName
                },
                Blogs = writer.Blogs?.Select(blog => new BlogDto
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    Comments = blog.Comments?.Select(c => new CommentDto
                    {
                        Id = c.Id,
                        Content = c.Content
                    }).ToList() ?? new List<CommentDto>()
                }).ToList() ?? new List<BlogDto>()
            };
        }
    }
}
