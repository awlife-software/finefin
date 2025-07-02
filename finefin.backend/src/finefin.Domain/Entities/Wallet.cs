using finefin.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public WalletType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public WalletColor Color { get; set; }
        public decimal Balance { get; set; } = decimal.Zero;
        public DateTime LastEditDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual LocalUser? User { get; set; }
        public Guid UserId { get; set; }
    }
}
