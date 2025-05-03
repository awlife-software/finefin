using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.TransactionServices.Update
{
    public class UpdateTransactionService : IUpdateTransactionService
    {
        public Task<CompleteTransactionResponse> CompleteTransaction(string userId, string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
