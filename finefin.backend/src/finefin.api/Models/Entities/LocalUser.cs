using valet.lib.Auth.Domain.Entities;

namespace finefin.api.Models.Entities
{
    public class LocalUser : User
    {
        public ICollection<Wallet> Wallets { get; set; } = [];
    }
}
