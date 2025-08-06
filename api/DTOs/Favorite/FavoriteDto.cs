using api.DTOs.Blog;
using api.DTOs.Writer;

namespace api.DTOs.Favorite
{
    public class FavoriteDto
    {
        public WriterDto Writer { get; set; } = null!;
        public BlogDto Blog { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
