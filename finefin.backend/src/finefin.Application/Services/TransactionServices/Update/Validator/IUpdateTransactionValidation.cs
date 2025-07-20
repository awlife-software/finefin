using finefin.Shared.Communication.Requests.Transaction;
using FluentValidation;

namespace finefin.Application.Services.TransactionServices.Update.Validator
{
    public interface IUpdateTransactionValidation : IValidator<UpdateTransactionRequest>
    {
    }
}
