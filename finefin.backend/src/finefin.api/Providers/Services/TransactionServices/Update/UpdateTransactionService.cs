using Azure.Core;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using finefin.api.Models.Entities;
using finefin.api.Models.Enums;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Providers.Services.TransactionServices.Update
{
    public class UpdateTransactionService : IUpdateTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;
        private readonly IRecurrenceRepository _recurrenceRepository;

        public UpdateTransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IWalletRepository walletRepository, IRecurrenceRepository recurrenceRepository)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _walletRepository = walletRepository;
            _recurrenceRepository = recurrenceRepository;
        }

        public async Task CompleteTransaction(string userId, string transactionId)
        {
            var transaction = await _transactionRepository.GetAsync(x => x.Id.ToString() == transactionId) 
                ?? throw new InvalidIdException();

            var isValid = await _walletRepository.WalletBelongsToUser(transaction.WalletId, Guid.Parse(userId));

            if (!isValid)
                throw new WalletDontBelongToUserException();
            // VALIDATE BALANCE
            if (transaction.Type == TransactionType.EXPENSE.ToString())
                await ValidateCompletionOnExpense(transaction, transaction.WalletId);

            transaction.IsCompleted = true;
            transaction.CompletionDate = DateTime.UtcNow;

            _transactionRepository.Update(transaction);
            await _unitOfWork.Commit();
        }

        public async Task UpdateTransaction(string userId, UpdateTransactionRequest request)
        {
            // TODO: VALIDATE

            var entity = await _transactionRepository.GetTransactionWithDependencies(request.TransactionId)
                ?? throw new InvalidIdException();

            var isValid = await _walletRepository.WalletBelongsToUser(entity.WalletId, Guid.Parse(userId));

            if (!isValid)
                throw new WalletDontBelongToUserException();

            if (entity.Type == TransactionType.EXPENSE.ToString())
                await ValidateExpense(request, entity.WalletId);


            // HANDLE RECURRENCES
            entity.HandleCompetionAndBalance(request);
            entity.HandleFirstOccurrence(request);

            _transactionRepository.Update(entity);

            if (entity.Recurrence!.Occurrences > 1 && request.RecurrenceOption == UpdateRecurrenceOption.All)
            {
                var list = await _transactionRepository.GetTransactionListWithDependencies(request.TransactionId, entity.DueDate.Date);

                var index = 1;

                list.ForEach(x => 
                {
                    x.HandleRecurrences(request, index);
                    index++;
                });

                _transactionRepository.UpdateRange(list);
            }

            await _unitOfWork.Commit();
        }
        // TODO: OTIMIZAR E REAPROVEITAR MÉTODOS
        private async Task ValidateExpense(UpdateTransactionRequest request, Guid walletId)
        {
            if (request.IsCompleted)
            {
                var balance = await _walletRepository.GetWalletBalance(walletId);

                if (request.Amount > balance)
                    throw new InsufficientFundsException();
            }
        }

        private async Task ValidateCompletionOnExpense(Transaction transaction, Guid walletId)
        {
            var balance = await _walletRepository.GetWalletBalance(walletId);

            if (transaction.Amount > balance)
                throw new InsufficientFundsException();

        }
    }
}
