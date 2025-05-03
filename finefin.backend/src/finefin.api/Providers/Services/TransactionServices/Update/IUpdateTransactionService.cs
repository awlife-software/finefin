using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.TransactionServices.Update
{
    public interface IUpdateTransactionService
    {
        Task<CompleteTransactionResponse> CompleteTransaction(string userId, string transactionId);
    }
}
