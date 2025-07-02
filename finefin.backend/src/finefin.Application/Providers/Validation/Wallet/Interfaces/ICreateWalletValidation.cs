using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.Providers.Validation.Wallet.Interfaces
{
    public interface ICreateWalletValidation : IValidator<CreateWalletRequest>
    {
    }
}
