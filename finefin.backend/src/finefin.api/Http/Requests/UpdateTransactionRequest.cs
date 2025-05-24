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
        public decimal Amount { get; set; } = decimal.Zero; 
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
        public string Description { get; set; } = string.Empty;

    }

    public enum UpdateRecurrenceOption
    {
        This = 0,
        All = 1,
    }
}
