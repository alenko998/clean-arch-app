using api.DTOs.UserInfo;

namespace api.DTOs.AccountUser
{
    public class AccountUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserInfoDto? UserInfo { get; set; }
    }
}
