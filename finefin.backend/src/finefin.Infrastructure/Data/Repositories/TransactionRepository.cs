using finefin.Domain.Entities;
using finefin.Domain.Entities.Enums;
using finefin.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using valet.lib.Core.Data.Repositories;

namespace finefin.Infrastructure.Data.Repositories
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

        public async Task<List<Transaction>> GetRecurrenceTransactions(Guid recurrenceId, DateTime dueDate) => await dbSet
            .Where(x => x.RecurrenceId == recurrenceId && !x.IsCompleted && x.DueDate.Date > dueDate)
            .ToListAsync();

        public async Task<Transaction> GetTransactionWithDependencies(Guid transactionId) => await dbSet
            .Include(x => x.Wallet)
            .Include(x => x.Recurrence)
            .FirstAsync(x => x.Id == transactionId);

        public async Task<List<Transaction>> GetAllPendingTransactionFromRecurrence(Guid recurrenceId) => await dbSet
            .Include(x => x.Wallet)
            .Include(x => x.Recurrence)
            .Where(x => x.RecurrenceId == recurrenceId && !x.IsCompleted)
            .ToListAsync();

        public async Task<decimal> GetMonthTotalIncomes(Guid userId, DateTime date) => await dbSet
            .Include(x => x.Wallet)
            .Where(x => x.Wallet!.UserId.Equals(userId) && x.DueDate.Month.Equals(date.Month) && x.DueDate.Year.Equals(date.Year) && x.Type == TransactionType.INCOME && x.IsCompleted)
            .SumAsync(x => x.Amount);

        public async Task<decimal> GetMonthTotalExpenses(Guid userId, DateTime date) => await dbSet
            .Include(x => x.Wallet)
            .Where(x => x.Wallet!.UserId.Equals(userId) && x.DueDate.Month.Equals(date.Month) && x.DueDate.Year.Equals(date.Year) && x.Type == TransactionType.EXPENSE && x.IsCompleted)
            .SumAsync(x => x.Amount);

        // UPDATE RANGE
        public void UpdateRange(List<Transaction> transactions)
        {
            dbSet.UpdateRange(transactions);
        }
    }
}
