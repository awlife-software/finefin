using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Models.Enums;

namespace finefin.api.Providers.Services.TransactionServices.Update
{
    public static class UpdateTransactionServiceHelper
    {
        public static void HandleCompetionAndBalance(this Transaction entity, UpdateTransactionRequest request)
        {
            if (entity.IsCompleted && !request.IsCompleted)
            {
                entity.IsCompleted = false;
                entity.CompletionDate = DateTime.MinValue;

                if (entity.Type == TransactionType.INCOME.ToString())
                    entity.Wallet!.Balance -= entity.Amount;// SUBTRAI O VALOR PERSISTIDO // TODO: TESTAR SE A MANIPULAÇÃO DE SALDO DIRETA VAI ROLAR
                else 
                    entity.Wallet!.Balance += entity.Amount;
                entity.Amount = request.Amount;
            }

            if (!entity.IsCompleted && request.IsCompleted)
            {
                entity.IsCompleted = true; // complete
                entity.CompletionDate = DateTime.UtcNow;
                // HANDLE AMOUNT HERE
                entity.Amount = request.Amount; // 20 -> 30
                if (entity.Type == TransactionType.INCOME.ToString())
                    entity.Wallet!.Balance += entity.Amount; // +30
                else
                    entity.Wallet!.Balance -= entity.Amount;
            }

            if (entity.IsCompleted.Equals(request.IsCompleted)) // equal
            {
                if (!entity.Amount.Equals(request.Amount)) // equal
                {
                    if (entity.IsCompleted)
                    {
                        if (entity.Type == TransactionType.INCOME.ToString())
                            entity.Wallet!.Balance -= entity.Amount;
                        else
                            entity.Wallet!.Balance += entity.Amount;

                        entity.Amount = request.Amount;

                        if (entity.Type == TransactionType.INCOME.ToString())
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
    }
}
