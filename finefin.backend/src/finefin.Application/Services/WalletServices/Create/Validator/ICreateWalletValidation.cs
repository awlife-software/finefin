using finefin.Shared.Communication.Requests.Wallet;
using FluentValidation;

namespace finefin.Application.Services.WalletServices.Create.Validator
{
    public interface ICreateWalletValidation : IValidator<CreateWalletRequest>
    {
    }
}
