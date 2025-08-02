using finefin.Application.Services.TransactionServices.Update.Validator;
using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using finefin.Shared.Communication.Requests.Transaction;
using finefin.Shared.Exceptions;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Application.Services.TransactionServices.Update
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

        public async Task Complete(string userId, string transactionId)
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
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(string userId, UpdateTransactionRequest request)
        {
            await Validate(request); // TODO: CHECK IF NEGATIVE AMOUNT IS ALLOWED

            var transactions = new List<Transaction>();

            var transaction = await _transactionRepository.GetTransactionWithDependencies(request.TransactionId)
                ?? throw new InvalidIdException();

            if (!await _walletRepository.WalletBelongsToUser(transaction.WalletId, Guid.Parse(userId)))
                throw new WalletDontBelongToUserException();

            var wallet = await _walletRepository.GetAsync(x => x.Id == transaction.WalletId)
                ?? throw new InvalidIdException();

            HandleBalance(transaction, wallet, request);
            // TODO: HANDLE CHANGES
            transactions.Add(transaction);

            if (request.AllPendingTransactions)
            {
                var pendingTransactions = await _transactionRepository.GetAllPendingTransactionFromRecurrence(transaction.RecurrenceId);
                // TODO: HANDLE CHANGES FOR PENDING TRANSACTIONS
                pendingTransactions.ForEach(x => transactions.Add(x));
            }

            _transactionRepository.UpdateRange(transactions);

            await _unitOfWork.CommitAsync();
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

        private void HandleBalance(Transaction transaction, Wallet wallet, UpdateTransactionRequest request)
        {
            if (transaction.IsCompleted && !request.IsCompleted)
            {
                wallet.HandleBalanceOnCancellation(transaction);
                transaction.Revert();
                return;
            }
            if (!transaction.IsCompleted && request.IsCompleted)
            {
                wallet.HandleBalanceOnCompletion(transaction);
                transaction.Complete();
                return;
            }
            if (!transaction.Amount.Equals(request.Amount) && transaction.IsCompleted)
            {
                wallet.OverwriteTransactionAmount(transaction, request.Amount);
                return;
            }
        }
    }
}
