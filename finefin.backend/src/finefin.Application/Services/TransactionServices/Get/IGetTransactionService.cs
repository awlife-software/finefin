using finefin.Shared.Communication.Responses.Transaction;

namespace finefin.Application.Services.TransactionServices.Get
{
    public interface IGetTransactionService
    {
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForUser(string userId);
        Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForWallet(string walletId, string userId);
    }
}
