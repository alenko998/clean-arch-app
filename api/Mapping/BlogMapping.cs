using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Blog;
using api.DTOs.Comment;
using Domain.Entities;

namespace api.Mapping
{
    public static class BlogMapping
    {
        public static Blog ToEntity(CreateBlogDto dto)
        {
            return new Blog
            {
                Title = dto.Title,
                Content = dto.Content
            };
        }

        public static Blog ToEntityFromUpdate(UpdateBlogDto dto)
        {
            return new Blog
            {
                Title = dto.Title,
                Content = dto.Content
            };
        }

        public static BlogDto ToDto(Blog blog)
        {
            return new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Comments = blog.Comments?.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content
                }).ToList() ?? new List<CommentDto>()
            };
        }
    }


}