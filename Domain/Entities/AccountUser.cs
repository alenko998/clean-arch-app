namespace Domain.Entities
{
    public class AccountUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? UserInfoId { get; set; }           // Foreign key
        public UserInfo? UserInfo { get; set; }
    }
}
