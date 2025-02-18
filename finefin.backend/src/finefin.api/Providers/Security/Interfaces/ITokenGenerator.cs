using finefin.api.Models.Entities;

namespace finefin.api.Providers.Security.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
