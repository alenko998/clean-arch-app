namespace Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        // ✅ NOVO: veza ka Writer
        public int WriterId { get; set; }
        public Writer Writer { get; set; } = null!;
    }
}
