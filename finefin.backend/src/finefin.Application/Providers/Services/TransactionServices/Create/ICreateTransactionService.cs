using finefin.Shared.Communication.Requests;

namespace finefin.Application.Providers.Services.TransactionServices.Create
{
    public interface ICreateTransactionService
    {
        Task CreateTransaction(string userId, CreateTransactionRequest request);
    }
}
