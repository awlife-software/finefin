using Bogus;
using finefin.api.Http.Requests;
using finefin.api.Models.Enums;

namespace finefin.test._03._Builders.Requests
{
    public static class CreateWalletRequestBuilder
    {
        public static CreateWalletRequest Build()
        {
            return new Faker<CreateWalletRequest>()
                .RuleFor(wallet => wallet.Name, (f) => f.Lorem.Word())
                .RuleFor(wallet => wallet.Color, WalletColor.RED.ToString())
                .RuleFor(wallet => wallet.Type, WalletType.CHECKING.ToString())
                .RuleFor(wallet => wallet.Balance, (f) => f.Finance.Amount());
        }
    }
}
