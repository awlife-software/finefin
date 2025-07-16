namespace finefin.Shared.Communication.Requests
{
    public class UpdateTransactionRequest
    {
        public Guid TransactionId { get; set; }
        public bool IsCompleted { get; set; }
        public decimal Amount { get; set; } = decimal.Zero;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public string Description { get; set; } = string.Empty;
    }
}
