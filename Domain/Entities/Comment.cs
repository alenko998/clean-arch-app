

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; } // Primarni ključ
        public string Content { get; set; } = string.Empty; // Tekst komentara
        public int BlogId { get; set; } // FK
        public Blog Blog { get; set; } = null!;
    }
}
