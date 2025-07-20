using System.ComponentModel.DataAnnotations;

namespace finefin.Shared.Communication.Responses.Transaction
{
    public class PendingTransactionResponse
    {
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; }
        public string RecurrenceType { get; set; } = string.Empty;
        public int Occurrences { get; set; } = 1;
        public Guid? RecurrenceId { get; set; }
        public Guid WalletId { get; set; }
    }
}
