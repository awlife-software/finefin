using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.Transaction.Create.Validator
{
    public interface ICreateTransactionValidation : IValidator<CreateTransactionRequest>
    {
    }
}
