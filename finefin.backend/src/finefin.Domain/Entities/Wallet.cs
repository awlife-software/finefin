using finefin.Domain.Entities.Enums;
using finefin.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        protected Wallet() { }
        public Wallet(WalletType type, string name, WalletColor collor, decimal balance, Guid userId)
        {
            this.Type = type;
            SetName(name);
            this.Color = collor;
            SetInitialBalance(balance);
            this.UserId = userId;
            this.Transactions = [];
        }

        public WalletType Type { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public WalletColor Color { get; private set; }
        public decimal Balance { get; private set; } = decimal.Zero;
        public virtual ICollection<Transaction> Transactions { get; private set; }
        public virtual LocalUser? User { get; private set; }
        public Guid UserId { get; private set; }

        public void Rename(string newName)
        {
            SetName(newName);
            Touch();
        }

        public void ChangeColor(WalletColor newColor)
        {
            this.Color = newColor;
            Touch();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Wallet name cannot be empty.", nameof(name));
            this.Name = name;
        }

        private void SetInitialBalance(decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException("Initial balance cannot be negative.", nameof(balance));
            this.Balance = balance;
        }

        internal void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Income amount must be greater than zero.", nameof(amount));
            this.Balance += amount;
            Touch();
        }

        internal void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Expense amount must be greater than zero.", nameof(amount));
            this.Balance -= amount;
            Touch();
        }

        public void HandleBalanceOnCompletion(Transaction transaction)
        {
            if (transaction.Type == TransactionType.EXPENSE && transaction.Amount > Balance)
                throw new InsufficientFundsException();

            Balance += transaction.Type == TransactionType.INCOME ? transaction.Amount : -transaction.Amount;

            Touch();
        }

        private void Touch()
        {
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
