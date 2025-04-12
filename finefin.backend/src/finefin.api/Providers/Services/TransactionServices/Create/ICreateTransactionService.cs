using finefin.api.Http.Requests;

namespace finefin.api.Providers.Services.TransactionServices.Create
{
    public interface ICreateTransactionService
    {
        Task CreateTransaction(string userId, CreateTransactionRequest request);
    }
}
