namespace finefin.Shared.Communication.Requests
{
    public class CreateTransactionRequest
    {
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; }
        public RecurrenceRequest Recurrence { get; set; } = new();
        public Guid WalletId { get; set; }
    }

    public class RecurrenceRequest
    {
        public string Type { get; set; } = string.Empty;
        public int Occurrences { get; set; } = 1;
    }
}
