using finefin.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        [EnumDataType(typeof(WalletType))]
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        [EnumDataType(typeof(WalletColor))]
        public string Color { get; set; } = string.Empty;
        public decimal Balance { get; set; } = decimal.Zero;
        public DateTime LastEditDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual LocalUser? User { get; set; }
        public Guid UserId { get; set; }
    }
}
