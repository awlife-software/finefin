using finefin.Shared.Communication.Responses;

namespace finefin.Application.UseCases.Transaction.Search
{
    public interface ISearchTransaction
    {
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForUser(string userId);
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForWallet(string walletId, string userId);
    }
}
