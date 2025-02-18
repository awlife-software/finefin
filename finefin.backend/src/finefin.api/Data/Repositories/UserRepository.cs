using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace finefin.api.Data.Repositories
{
    public class UserRepository(AppDbContext db) : Repository<User>(db), IUserRepository
    {
        public async Task<bool> UserExists(string email) => await _db.Users.AnyAsync(u => u.Email.Equals(email));
        public async Task<User> GetUserWithRolesAsync(string email) => await dbSet.Include(u => u.UserRoles)
                                                                            .ThenInclude(ur => ur.Role)
                                                                            .FirstAsync(u => u.Email.Equals(email));
    }
}
