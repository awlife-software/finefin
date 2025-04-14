using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace finefin.api.Models.Entities
{
    public class Transaction : BaseEntity
    {
        [EnumDataType(typeof(TransactionType))]
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; }
        public virtual Recurrence? Recurrence { get; set; }
        public Guid? RecurrenceId { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public Guid WalletId { get; set; }

    }
}
