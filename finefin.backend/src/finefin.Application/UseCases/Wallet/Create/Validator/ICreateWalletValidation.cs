using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.Wallet.Create.Validator
{
    public interface ICreateWalletValidation : IValidator<CreateWalletRequest>
    {
    }
}
