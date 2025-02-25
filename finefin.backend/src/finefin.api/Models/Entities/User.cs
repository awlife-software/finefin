using clauth.lib.Core.Entities;

namespace finefin.api.Models.Entities
{
    public class User : ClauthUser
    {
        public ICollection<Wallet> Wallets { get; set; } = [];
    }
}
