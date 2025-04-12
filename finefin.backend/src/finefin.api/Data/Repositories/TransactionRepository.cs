using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;
using valet.lib.Core.Data.Repositories;

namespace finefin.api.Data.Repositories
{
    public class TransactionRepository(AppDbContext db) : Repository<Transaction>(db), ITransactionRepository
    {
    }
}
