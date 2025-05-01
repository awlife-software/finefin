using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.TransactionServices.Get
{
    public interface IGetTransactionService
    {
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForUser(string userId);
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForWallet(string walletId);
    }
}
