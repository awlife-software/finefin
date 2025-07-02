using finefin.Shared.Communication.Requests;

namespace finefin.Application.Providers.Services.TransactionServices.Update
{
    public interface IUpdateTransactionService
    {
        Task CompleteTransaction(string userId, string transactionId);
        Task UpdateTransaction(string userId, UpdateTransactionRequest request);
    }
}
