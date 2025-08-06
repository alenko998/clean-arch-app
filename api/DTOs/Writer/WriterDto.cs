using api.DTOs.UserInfo;

namespace api.DTOs.Writer
{
    public class WriterDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public UserInfoDto? UserInfo { get; set; }
    }
}
