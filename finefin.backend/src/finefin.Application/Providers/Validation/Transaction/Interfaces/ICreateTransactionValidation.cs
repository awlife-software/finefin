using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.Providers.Validation.Transaction.Interfaces
{
    public interface ICreateTransactionValidation : IValidator<CreateTransactionRequest>
    {
    }
}
