using valet.lib.Auth.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class LocalUser : User
    {
        public ICollection<Wallet> Wallets { get; set; } = [];
    }
}
