using finefin.Application.Providers.Validation.Transaction.Interfaces;
using finefin.Domain.Entities.Enums;
using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using finefin.Shared.Communication.Requests;
using finefin.Shared.Exceptions;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Application.Providers.Services.TransactionServices.Update
{
    public class UpdateTransactionService : IUpdateTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;
        private readonly IRecurrenceRepository _recurrenceRepository;
        private readonly IUpdateTransactionValidation _validator;

        public UpdateTransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IWalletRepository walletRepository, IRecurrenceRepository recurrenceRepository, IUpdateTransactionValidation validator)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _walletRepository = walletRepository;
            _recurrenceRepository = recurrenceRepository;
            _validator = validator;
        }

        public async Task CompleteTransaction(string userId, string transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionWithDependencies(Guid.Parse(transactionId))
                ?? throw new InvalidIdException();

            if (transaction.IsCompleted)
                throw new TransactionAlreadyCompletedException();

            if (!await _walletRepository.WalletBelongsToUser(transaction.WalletId, Guid.Parse(userId)))
                throw new WalletDontBelongToUserException();

            var wallet = await _walletRepository.GetAsync(x => x.Id == transaction.WalletId)
                ?? throw new InvalidIdException();

            wallet.HandleBalanceOnCompletion(transaction);
            transaction.Complete();

            _transactionRepository.Update(transaction);
            await _unitOfWork.Commit();
        }

        public async Task UpdateTransaction(string userId, UpdateTransactionRequest request)
        {
            await Validate(request); // TODO: CHECK IF NEGATIVE AMOUNT IS ALLOWED

            var transaction = await _transactionRepository.GetTransactionWithDependencies(request.TransactionId)
                ?? throw new InvalidIdException();

            if (!await _walletRepository.WalletBelongsToUser(transaction.WalletId, Guid.Parse(userId)))
                throw new WalletDontBelongToUserException();

            var wallet = await _walletRepository.GetAsync(x => x.Id == transaction.WalletId)
                ?? throw new InvalidIdException();

            if (transaction.IsCompleted && !request.IsCompleted)
            {
                wallet.HandleBalanceOnCancellation(transaction);
                transaction.Revert();
                // TODO: UPDATE BALANCE AFTER REVERTING
            }
            if (!transaction.IsCompleted && request.IsCompleted)
            {
                // TODO: UPDATE BALANCE BEFORE COMPLETING
                wallet.HandleBalanceOnCompletion(transaction);
                transaction.Complete();
            }
                

            _transactionRepository.Update(transaction);

            if (transaction.Recurrence!.Occurrences > 1 && request.RecurrenceOption == UpdateRecurrenceOption.All.ToString())
            {
                var list = await _transactionRepository.GetRecurrenceTransactions(transaction.RecurrenceId, transaction.DueDate.Date);

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
        private async Task ValidateExpense(decimal amount, bool isCompleted, Guid walletId)
        {
            if (isCompleted)
            {
                var balance = await _walletRepository.GetWalletBalance(walletId);

                if (amount > balance)
                    throw new InsufficientFundsException();
            }
        }

        private async Task Validate(UpdateTransactionRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }


    }
}
