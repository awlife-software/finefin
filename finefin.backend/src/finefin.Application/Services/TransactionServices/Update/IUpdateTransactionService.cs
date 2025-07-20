using finefin.Shared.Communication.Requests.Transaction;

namespace finefin.Application.Services.TransactionServices.Update
{
    public interface IUpdateTransactionService
    {
        Task Complete(string userId, string transactionId);
        Task Update(string userId, UpdateTransactionRequest request);
    }
}
