using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;

namespace finefin.api.Data.Repositories
{
    public class UserRoleRepository(AppDbContext db) : Repository<UserRole>(db), IUserRoleRepository
    {
    }
}
