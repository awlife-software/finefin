using finefin.api.Models.Entities;
using finefin.api.Models.Enums;

namespace finefin.api.Http.Requests
{
    public class CreateTransactionRequest
    {
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; }
        public virtual RecurrencyRequest? Recurrency { get; set; }
        public Guid WalletId { get; set; }
    }

    public class RecurrencyRequest
    {
        public string Type { get; set; } = RecurrencyType.SINGLE.ToString();
        public int Occurrences { get; set; } = 1;
    }
}
