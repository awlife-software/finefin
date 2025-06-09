using finefin.api.Http.Requests;
using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.TransactionServices.Update
{
    public interface IUpdateTransactionService
    {
        Task CompleteTransaction(string userId, string transactionId);
        Task UpdateTransaction(string userId, UpdateTransactionRequest request);
    }
}
