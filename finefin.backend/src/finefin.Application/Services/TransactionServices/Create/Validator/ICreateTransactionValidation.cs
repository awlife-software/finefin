using finefin.Shared.Communication.Requests.Transaction;
using FluentValidation;

namespace finefin.Application.Services.TransactionServices.Create.Validator
{
    public interface ICreateTransactionValidation : IValidator<CreateTransactionRequest>
    {
    }
}
