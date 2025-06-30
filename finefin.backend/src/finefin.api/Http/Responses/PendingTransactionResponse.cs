using finefin.api.Models.Entities;
using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace finefin.api.Http.Responses
{
    public class PendingTransactionResponse
    {
        [EnumDataType(typeof(TransactionType))]
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; }

        [EnumDataType(typeof(RecurrenceType))]
        public string RecurrenceType { get; set; } = string.Empty;
        public int Occurrences { get; set; } = 1;
        public Guid? RecurrenceId { get; set; }
        public Guid WalletId { get; set; }
    }
}
