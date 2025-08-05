using api.DTOs.UserInfo;
using Domain.Entities;

namespace api.Mapping
{
    public static class UserInfoMapping
    {
        public static UserInfo ToEntity(CreateUserInfoDto dto)
        {
            return new UserInfo
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static UserInfo ToEntityFromUpdate(UpdateUserInfoDto dto)
        {
            return new UserInfo
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }
    }
}
