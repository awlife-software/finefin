using finefin.Domain.Entities;
using finefin.Domain.Entities.Enums;

namespace finefin.Domain.Services
{
    public static class RecurrenceTransactionService
    {
        public static void GenerateTransactionsForRecurrence(Recurrence recurrence, Transaction template, Wallet wallet)
        {
            if (recurrence == null) throw new ArgumentNullException(nameof(recurrence));
            if (template == null) throw new ArgumentNullException(nameof(template));
            if (wallet == null) throw new ArgumentNullException(nameof(wallet));

            for (int i = 0; i < recurrence.Occurrences; i++)
            {
                var transaction = new Transaction(
                    template.Description,
                    template.Type,
                    template.Amount,
                    CalculateDueDate(recurrence.Type, template.DueDate, i),
                    i == 0 ? template.IsCompleted : false,
                    template.CompletionDate,
                    wallet.Id
                );
                recurrence.Transactions.Add(transaction);

                if (i == 0 && transaction.IsCompleted)
                    HandleBalance(wallet, transaction);

            }
        }

        private static DateTime CalculateDueDate(RecurrenceType type, DateTime start, int index)
        {
            return type switch
            {
                RecurrenceType.DAYLI => start.AddDays(index),
                RecurrenceType.WEEKLY => start.AddDays(index * 7),
                RecurrenceType.MONTHLY => start.AddMonths(index),
                RecurrenceType.YEARLY => start.AddYears(index),
                _ => start
            };
        }

        private static void HandleBalance(Wallet wallet, Transaction transaction)
        {
            if (transaction.Type == TransactionType.INCOME)
                wallet.Deposit(transaction.Amount);
            else
                wallet.Withdraw(transaction.Amount);
        }
    }
}