using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace finefin.Infrastructure.Data.Repositories
{
    public class RecurrenceRepository(AppDbContext db) : Repository<Recurrence>(db), IRecurrenceRepository
    {
        public async Task<Recurrence> CreateAndGetAsync(Recurrence recurrence)
        {
            dbSet.Add(recurrence);

            await _db.SaveChangesAsync();

            await _db.Entry(recurrence).ReloadAsync();

            return recurrence;
        }
    }
}
