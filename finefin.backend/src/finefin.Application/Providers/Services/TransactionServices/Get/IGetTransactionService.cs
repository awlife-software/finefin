using finefin.Shared.Communication.Responses;

namespace finefin.Application.Providers.Services.TransactionServices.Get
{
    public interface IGetTransactionService
    {
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForUser(string userId);
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForWallet(string walletId, string userId);
    }
}
