using Bogus;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Models.Enums;

namespace finefin.test._03._Builders.Entities
{
    public class WalletBuilder
    {
        public static Wallet Build()
        {
            return new Faker<Wallet>()
                .RuleFor(wallet => wallet.Name, (f) => f.Lorem.Word())
                .RuleFor(wallet => wallet.Color, WalletColor.RED.ToString())
                .RuleFor(wallet => wallet.Type, WalletType.CHECKING.ToString())
                .RuleFor(wallet => wallet.Balance, (f) => f.Finance.Amount())
                .RuleFor(wallet => wallet.Id, (f) => f.Random.Guid())
                .RuleFor(wallet => wallet.UserId, (f) => f.Random.Guid());
        }
    }
}
