//using finefin.Application.Providers.Validation.Transaction.Interfaces;
//using finefin.Domain.Entities.Enums;
//using finefin.Domain.Entities;
//using finefin.Domain.Interfaces.Repositories;
//using finefin.Shared.Communication.Requests;
//using finefin.Shared.Exceptions;
//using valet.lib.Core.Domain.Interfaces;

//namespace finefin.Application.Providers.Services.TransactionServices.Update
//{
//    public class UpdateTransactionService : IUpdateTransactionService
//    {
//        private readonly ITransactionRepository _transactionRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IWalletRepository _walletRepository;
//        private readonly IRecurrenceRepository _recurrenceRepository;
//        private readonly IUpdateTransactionValidation _validator;

//        public UpdateTransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IWalletRepository walletRepository, IRecurrenceRepository recurrenceRepository, IUpdateTransactionValidation validator)
//        {
//            _transactionRepository = transactionRepository;
//            _unitOfWork = unitOfWork;
//            _walletRepository = walletRepository;
//            _recurrenceRepository = recurrenceRepository;
//            _validator = validator;
//        }

//        public async Task CompleteTransaction(string userId, string transactionId)
//        {
//            var transaction = await _transactionRepository.GetTransactionWithDependencies(Guid.Parse(transactionId)) 
//                ?? throw new InvalidIdException();

//            if (transaction.IsCompleted)
//                throw new TransactionAlreadyCompletedException();

//            if (!await _walletRepository.WalletBelongsToUser(transaction.WalletId, Guid.Parse(userId)))
//                throw new WalletDontBelongToUserException();

//            if (transaction.Type == TransactionType.EXPENSE)
//                await ValidateExpenseOnCompletion(transaction, transaction.WalletId);

//            transaction.HandleBalanceOnCompletion();

//            transaction.IsCompleted = true;
//            transaction.CompletionDate = DateTime.UtcNow;

//            _transactionRepository.Update(transaction);
//            await _unitOfWork.Commit();
//        }

//        public async Task UpdateTransaction(string userId, UpdateTransactionRequest request)
//        {
//            await Validate(request);

//            var entity = await _transactionRepository.GetTransactionWithDependencies(request.TransactionId)
//                ?? throw new InvalidIdException();

//            if (!await _walletRepository.WalletBelongsToUser(entity.WalletId, Guid.Parse(userId)))
//                throw new WalletDontBelongToUserException();

//            if (entity.Type == TransactionType.EXPENSE)
//                await ValidateExpense(request, entity.WalletId);

//            entity.HandleCompetionAndBalance(request);
//            entity.HandleFirstOccurrence(request);

//            _transactionRepository.Update(entity);

//            if (entity.Recurrence!.Occurrences > 1 && request.RecurrenceOption == UpdateRecurrenceOption.All.ToString())
//            {
//                var list = await _transactionRepository.GetRecurrenceTransactions(entity.RecurrenceId, entity.DueDate.Date);

//                var index = 1;

//                list.ForEach(x => 
//                {
//                    x.HandleRecurrences(request, index);
//                    index++;
//                });

//                _transactionRepository.UpdateRange(list);
//            }

//            await _unitOfWork.Commit();
//        }

//        // TODO: OTIMIZAR E REAPROVEITAR MÉTODOS
//        private async Task ValidateExpense(UpdateTransactionRequest request, Guid walletId)
//        {
//            if (request.IsCompleted)
//            {
//                var balance = await _walletRepository.GetWalletBalance(walletId);

//                if (request.Amount > balance)
//                    throw new InsufficientFundsException();
//            }
//        }

//        private async Task ValidateExpenseOnCompletion(Transaction transaction, Guid walletId)
//        {
//            var balance = await _walletRepository.GetWalletBalance(walletId);

//            if (transaction.Amount > balance)
//                throw new InsufficientFundsException();

//        }

//        private async Task Validate(UpdateTransactionRequest request)
//        {
//            var result = await _validator.ValidateAsync(request);

//            if (!result.IsValid)
//            {
//                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
//                throw new ErrorOnValidationException(errors);
//            }
//        }


//    }
//}
