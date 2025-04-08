using finefin.api.Http.Requests;
using FluentValidation;

namespace finefin.api.Providers.Validation.Wallet.Interfaces
{
    public interface ICreateWalletValidation : IValidator<CreateWalletRequest>
    {
    }
}
