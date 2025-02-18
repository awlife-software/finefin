using finefin.api.Models.Entities;

namespace finefin.api.Data.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> RoleExistsAsync(string name);
    }
}
