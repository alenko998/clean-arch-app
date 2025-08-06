using Domain.Entities;
using api.DTOs.Writer;
using api.DTOs.UserInfo;

namespace api.Mapping
{
    public static class WriterMapping
    {
        public static WriterDto ToDto(Writer writer)
        {
            return new WriterDto
            {
                Id = writer.Id,
                Username = writer.Username,
                UserInfo = writer.UserInfo is null ? null : new UserInfoDto
                {
                    Email = writer.UserInfo.Email,
                    FirstName = writer.UserInfo.FirstName,
                    LastName = writer.UserInfo.LastName
                }
            };
        }
    }
}
