using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.Transaction.Update.Validator
{
    public interface IUpdateTransactionValidation : IValidator<UpdateTransactionRequest>
    {
    }
}
