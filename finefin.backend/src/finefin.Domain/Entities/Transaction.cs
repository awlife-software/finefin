using finefin.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Transaction : BaseEntity
    {

        public Transaction(string description, TransactionType type, decimal amount, DateTime dueDate, bool isCompleted, DateTime completionDate, Guid walletId)
        {
            SetDescription(description);
            SetAmount(amount);
            this.Type = type;
            this.DueDate = dueDate;
            this.IsCompleted = isCompleted;
            HandleCompletionDate();
            this.WalletId = walletId;
        }

        public string Description { get; private set; } = string.Empty;
        public TransactionType Type { get; private set; }
        public decimal Amount { get; private set; } = decimal.Zero;
        public DateTime DueDate { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime CompletionDate { get; private set; }
        public virtual Recurrence? Recurrence { get; private set; }
        public Guid RecurrenceId { get; private set; }
        public virtual Wallet? Wallet { get; private set; }
        public Guid WalletId { get; private set; }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Transaction description cannot be empty.", nameof(description));
            this.Description = description;
            Touch();
        }

        private void SetAmount(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Transaction amount cannot be negative.", nameof(amount));
            this.Amount = amount;
            Touch();
        }

        private void HandleCompletionDate()
        {
            if (this.IsCompleted)
                this.CompletionDate = DateTime.UtcNow;
            else
                this.CompletionDate = DateTime.MinValue;
            Touch();
        }

        public void Complete()
        {
            IsCompleted = true;
            CompletionDate = DateTime.UtcNow;
            Touch();
        }

        public void Revert()
        {
            IsCompleted = false;
            CompletionDate = DateTime.MinValue;
            Touch();
        }

        public void UpdateAmount(decimal amount)
        {
            SetAmount(amount);
        }

        private void Touch()
        {
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
