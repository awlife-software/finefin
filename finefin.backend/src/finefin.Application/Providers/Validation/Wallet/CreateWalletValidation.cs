using finefin.Application.Providers.Validation.Wallet.Interfaces;
using finefin.Domain.Entities.Enums;
using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.Providers.Validation.Wallet
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
