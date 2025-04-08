using finefin.api.Http.Requests;
using finefin.api.Models.Enums;
using finefin.api.Providers.Validation.Wallet.Interfaces;
using FluentValidation;

namespace finefin.api.Providers.Validation.Wallet
{
    public class CreateWalletValidation : AbstractValidator<CreateWalletRequest>, ICreateWalletValidation
    {
        public CreateWalletValidation()
        {
            RuleFor(x => x.Type!)
                .Must(BeAValidType)
                .WithMessage(RSC.ResourceMessageException.WALLET_TYPE_INVALID);

            RuleFor(x => x.Name!)
                .MaximumLength(20)
                .MinimumLength(2)
                .WithMessage(RSC.ResourceMessageException.WALLET_NAME_LENGTH);

            RuleFor(x => x.Color!)
                .Must(BeAValidColor)
                .WithMessage(RSC.ResourceMessageException.WALLET_COLOR_INVALID);
        }

        public bool BeAValidType(string type) => Enum.TryParse<WalletType>(type, ignoreCase: true, out _);

        public bool BeAValidColor(string color) => Enum.TryParse<WalletColor>(color, ignoreCase: true, out _); 
    }
}
