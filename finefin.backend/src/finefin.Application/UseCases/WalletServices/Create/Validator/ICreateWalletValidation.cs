using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.WalletServices.Create.Validator
{
    public interface ICreateWalletValidation : IValidator<CreateWalletRequest>
    {
    }
}
