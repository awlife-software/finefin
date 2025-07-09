using finefin.Domain.Entities;
using finefin.Domain.Entities.Enums;
using finefin.Shared.Communication.Requests;

namespace finefin.Domain.Factories
{
    public static class TransactionFactory
    {
        public static Transaction CreateFromRequest(CreateTransactionRequest request)
        {
            return new Transaction(
                request.Description,
                Enum.Parse<TransactionType>(request.Type, true),
                request.Amount,
                request.DueDate,
                request.IsCompleted,
                request.IsCompleted ? DateTime.UtcNow : DateTime.MinValue,
                request.WalletId
                );
        }

    }
}
