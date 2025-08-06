using Domain.Entities;
using api.DTOs.Writer;

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
                Password = writer.Password
            };
        }
    }
}
