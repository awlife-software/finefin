using finefin.Shared.Communication.Requests;

namespace finefin.Application.UseCases.Transaction.Create
{
    public interface ICreateTransaction
    {
        Task Execute(string userId, CreateTransactionRequest request);
    }
}
