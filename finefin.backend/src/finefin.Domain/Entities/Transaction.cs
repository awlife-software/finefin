using finefin.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; set; } = string.Empty;

        [EnumDataType(typeof(TransactionType))]
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; }
        public DateTime CompletionDate { get; set; }
        public virtual Recurrence? Recurrence { get; set; }
        public Guid RecurrenceId { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public Guid WalletId { get; set; }

    }
}
