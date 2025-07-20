using finefin.Application.UseCases.Transaction.Update.Validator;
using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using finefin.Shared.Communication.Requests;
using finefin.Shared.Exceptions;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Application.UseCases.Transaction.Update
{
    public class UpdateTransaction : IUpdateTransaction
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;
        private readonly IRecurrenceRepository _recurrenceRepository;
        private readonly IUpdateTransactionValidation _validator;

        public UpdateTransaction(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IWalletRepository walletRepository, IRecurrenceRepository recurrenceRepository, IUpdateTransactionValidation validator)
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

            HandleBalance(transaction, wallet, request);

            _transactionRepository.Update(transaction);

            await _unitOfWork.Commit();
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

        private void HandleBalance(Domain.Entities.Transaction transaction, Domain.Entities.Wallet wallet, UpdateTransactionRequest request)
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
