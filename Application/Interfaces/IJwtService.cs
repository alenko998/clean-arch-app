using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Writer writer);
    }
}
