using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;
using valet.lib.Core.Data.Repositories;

namespace finefin.api.Data.Repositories
{
    public class RecurrencyRepository(AppDbContext db) : Repository<Recurrency>(db), IRecurrencyRepository
    {
        public async Task<Recurrency> CreateAndGetAsync(Recurrency recurrency)
        {
            dbSet.Add(recurrency);

            await _db.SaveChangesAsync();

            await _db.Entry(recurrency).ReloadAsync();

            return recurrency;
        }
    }
}
