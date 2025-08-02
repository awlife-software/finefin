using valet.lib.Auth.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class LocalUser : User
    {
        public LocalUser(string firstName, string lastName, string email, string password) : base(firstName,lastName,email,password)
        {
        }
        public ICollection<Wallet> Wallets { get; set; } = [];
    }
}
