using finefin.Shared.Communication.Requests;

namespace finefin.Application.UseCases.Transaction.Update
{
    public interface IUpdateTransaction
    {
        Task CompleteTransaction(string userId, string transactionId);
        Task UpdateTransaction(string userId, UpdateTransactionRequest request);
    }
}
