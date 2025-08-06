namespace Domain.Entities
{
    public class Favorite
    {
        public int WriterId { get; set; }
        public Writer Writer { get; set; } = null!;

        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
