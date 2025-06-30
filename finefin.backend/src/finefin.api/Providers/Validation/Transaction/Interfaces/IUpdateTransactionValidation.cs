using finefin.api.Http.Requests;
using FluentValidation;

namespace finefin.api.Providers.Validation.Transaction.Interfaces
{
    public interface IUpdateTransactionValidation : IValidator<UpdateTransactionRequest>
    {
    }
}
