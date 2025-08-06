using api.DTOs.Favorite;
using Domain.Entities;

namespace api.Mapping
{
    public static class FavoriteMapping
    {
        public static FavoriteDto ToDto(Favorite favorite)
        {
            return new FavoriteDto
            {
                Writer = WriterMapping.ToDto(favorite.Writer),
                Blog = BlogMapping.ToDto(favorite.Blog),
                CreatedAt = favorite.CreatedAt
            };
        }
    }
}
