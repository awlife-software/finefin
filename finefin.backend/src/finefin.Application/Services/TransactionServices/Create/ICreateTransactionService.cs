using finefin.Shared.Communication.Requests.Transaction;

namespace finefin.Application.Services.TransactionServices.Create
{
    public interface ICreateTransactionService
    {
        Task Create(string userId, CreateTransactionRequest request);
    }
}
