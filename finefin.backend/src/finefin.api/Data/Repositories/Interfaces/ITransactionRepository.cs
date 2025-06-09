using finefin.api.Models.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetAllPendingTransactionsForUserAsync(Guid userId);
        Task<List<Transaction>> GetAllPendingTransactionsForWalletAsync(Guid walletId);
        Task<Transaction> GetTransactionWithDependencies(Guid transactionId);
        Task<List<Transaction>> GetRecurrenceTransactions(Guid recurrenceId, DateTime dueDate);
        void UpdateRange(List<Transaction> transactions);
    }
}
