using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Data.Repositories;

namespace finefin.api.Data.Repositories
{
    public class TransactionRepository(AppDbContext db) : Repository<Transaction>(db), ITransactionRepository
    {
        public async Task<List<Transaction>> GetAllPendingTransactionsForUserAsync(Guid userId) => await dbSet
            .Include(x => x.Wallet)
            .Include(x => x.Recurrence)
            .Where(x => !x.IsCompleted && x.DueDate.Date <= DateTime.Today && x.Wallet!.UserId == userId).ToListAsync();

        public async Task<List<Transaction>> GetAllPendingTransactionsForWalletAsync(Guid walletId) => await dbSet
            .Include(x => x.Recurrence)
            .Where(x => !x.IsCompleted && x.DueDate.Date <= DateTime.Today && x.WalletId == walletId).ToListAsync();

        public async Task<Transaction> GetTransactionWithDependencies(Guid transactionId) => await dbSet
            .Include(x => x.Wallet)
            .Include(x => x.Recurrence)
            .FirstAsync(x => x.Id == transactionId);
    }
}
