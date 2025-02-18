using finefin.api.Models.Entities;

namespace finefin.api.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExists(string email);
        Task<User> GetUserWithRolesAsync(string email);
    }
}
