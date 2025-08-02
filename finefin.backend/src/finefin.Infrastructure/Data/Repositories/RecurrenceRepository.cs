using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Recurrence> GetRecurrenceWithDependencies(Guid recurrenceId) => await dbSet
            .Include(x => x.Transactions)
            .FirstAsync(x => x.Id == recurrenceId);
    }
}
