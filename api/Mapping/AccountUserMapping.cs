using api.DTOs.AccountUser;
using api.DTOs.UserInfo;
using Domain.Entities;

namespace api.Mapping
{
    public static class AccountUserMapping
    {
        public static AccountUser ToEntity(CreateAccountUserDto dto)
        {
            return new AccountUser
            {
                Username = dto.Username,
                Password = dto.Password
            };
        }

        public static AccountUser ToEntityFromUpdate(UpdateAccountUserDto dto)
        {
            return new AccountUser
            {
                Username = dto.Username,
                Password = dto.Password
            };
        }

        public static AccountUserDto ToDto(AccountUser user)
        {
            return new AccountUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                UserInfo = user.UserInfo is null ? null : new UserInfoDto
                {
                    Email = user.UserInfo.Email,
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName
                }
            };
        }

    }
}
