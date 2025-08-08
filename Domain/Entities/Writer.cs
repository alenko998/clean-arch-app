namespace Domain.Entities
{
    public class Writer
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public int? UserInfoId { get; set; }
        public UserInfo? UserInfo { get; set; }

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        // ✅ NOVO: jedan writer može imati više blogova
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
