namespace Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; } // EF automatski koristi kao primarni ključ
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new(); // Navigacija

    }
}
