using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace finefin.api.Models.Entities
{
    public class Transaction : BaseEntity
    {
        [EnumDataType(typeof(TransactionType))]
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; }
        public bool IsFixed { get; set; }
        public bool IsRecurring { get; set; }
        public virtual Recurrency? Recurrency { get; set; }
        public Guid? RecurrencyId { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public Guid WalletId { get; set; }

    }
}
