using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace finefin.api.Http.Requests
{
    public class UpdateTransactionRequest
    {
        public Guid TransactionId { get; set; }
        public UpdateRecurrenceOption RecurrenceOption { get; set; }

        // BALANCE HANDLE NEEDED
        public bool IsCompleted { get; set; } // HANDLE BALANCE CONSID

        [EnumDataType(typeof(TransactionType))]
        public string Type { get; set; } = string.Empty; // HANDLE BALANCE WHEN COMPLETED
        public decimal Amount { get; set; } = decimal.Zero; 
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public UpdateTransactionRequestRecurrence Recurrence { get; set; } = new();

    }

    public enum UpdateRecurrenceOption
    {
        This = 0,
        All = 1,
    }

    public class UpdateTransactionRequestRecurrence
    {
        public Guid Id { get; set; }

        [EnumDataType(typeof(RecurrenceType))]
        public string Type { get; set; } = string.Empty;
        public int Occurrences { get; set; } = 1;
    }
}
