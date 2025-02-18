using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace finefin.api.Data.Repositories
{
    public class RoleRepository(AppDbContext db) : Repository<Role>(db), IRoleRepository
    {
        public async Task<bool> RoleExistsAsync(string name) => await dbSet.AnyAsync(x => x.Name == name);
    }
}
