using finefin.Domain.Entities;
using finefin.Domain.Entities.Enums;
using finefin.Shared.Communication.Requests;

namespace finefin.Application.Providers.Services.TransactionServices.Update
{
    public static class UpdateTransactionServiceHelper
    {

        public static void HandleCompetionAndBalance(this Transaction entity, UpdateTransactionRequest request)
        {
            // UNCONFIRM TRANSACTION
            if (entity.IsCompleted && !request.IsCompleted)
            {
                entity.IsCompleted = false;
                entity.CompletionDate = DateTime.MinValue;

                if (entity.Type == TransactionType.INCOME)
                    entity.Wallet!.Balance -= entity.Amount;// SUBTRAI O VALOR PERSISTIDO // TODO: TESTAR SE A MANIPULAÇÃO DE SALDO DIRETA VAI ROLAR
                else
                    entity.Wallet!.Balance += entity.Amount;
                entity.Amount = request.Amount;
            }

            // CONFIRM TRANSACTION
            if (!entity.IsCompleted && request.IsCompleted)
            {
                entity.IsCompleted = true; // complete
                entity.CompletionDate = DateTime.UtcNow;
                // HANDLE AMOUNT HERE
                entity.Amount = request.Amount; // 20 -> 30
                if (entity.Type == TransactionType.INCOME)
                    entity.Wallet!.Balance += entity.Amount; // +30
                else
                    entity.Wallet!.Balance -= entity.Amount;
            }

            // MAITAIN TRANSACTION
            if (entity.IsCompleted.Equals(request.IsCompleted))
            {
                if (!entity.Amount.Equals(request.Amount)) // AMOUNT UPDATE CHECK
                {
                    if (entity.IsCompleted)
                    {
                        if (entity.Type == TransactionType.INCOME)
                            entity.Wallet!.Balance -= entity.Amount;
                        else
                            entity.Wallet!.Balance += entity.Amount;

                        entity.Amount = request.Amount;

                        if (entity.Type == TransactionType.INCOME)
                            entity.Wallet!.Balance += entity.Amount;
                        else
                            entity.Wallet!.Balance -= entity.Amount;
                    }
                    else
                    {
                        entity.Amount = request.Amount;
                    }
                }
            }

        }

        public static void HandleFirstOccurrence(this Transaction entity, UpdateTransactionRequest request)
        {
            entity.DueDate = request.DueDate;
            entity.Description = request.Description;
        }

        public static void HandleRecurrences(this Transaction entity, UpdateTransactionRequest request, int occurrence)
        {
            entity.DueDate = HandleOccurrenceDate(entity.Recurrence!.Type.ToString(), request.DueDate, occurrence);

            entity.Description = request.Description;

            entity.Amount = request.Amount;
        }

        private static DateTime HandleOccurrenceDate(string type, DateTime date, int index)
        {
            if (type == RecurrenceType.DAYLI.ToString())
                return date.AddDays(index);

            if (type == RecurrenceType.WEEKLY.ToString())
                return date.AddDays(index * 7);

            if (type == RecurrenceType.MONTHLY.ToString())
                return date.AddMonths(index);

            if (type == RecurrenceType.YEARLY.ToString())
                return date.AddYears(index);

            return date;
        }

        public static void HandleBalanceOnCompletion(this Transaction transaction)
        {
            if (transaction.Type == TransactionType.INCOME)
                transaction.Wallet!.Balance += transaction.Amount;
            else
                transaction.Wallet!.Balance -= transaction.Amount;
        }
    }
}
