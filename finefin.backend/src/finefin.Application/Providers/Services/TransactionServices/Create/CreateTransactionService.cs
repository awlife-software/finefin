using AutoMapper;
using finefin.Application.Providers.Validation.Transaction.Interfaces;
using finefin.Domain.Entities;
using finefin.Domain.Entities.Enums;
using finefin.Domain.Interfaces.Repositories;
using finefin.Domain.Services;
using finefin.Shared.Communication.Requests;
using finefin.Shared.Exceptions;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Application.Providers.Services.TransactionServices.Create
{
    public class CreateTransactionService : ICreateTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRecurrenceRepository _recurrenceRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICreateTransactionValidation _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTransactionService(ITransactionRepository transactionRepository, IRecurrenceRepository recurrenceRepository, IWalletRepository walletRepository, ICreateTransactionValidation validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _recurrenceRepository = recurrenceRepository;
            _walletRepository = walletRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateTransaction(string userId, CreateTransactionRequest request)
        {
            await Validate(request);

            var userIsValid = await _walletRepository.WalletBelongsToUser(request.WalletId, Guid.Parse(userId));

            if (!userIsValid)
                throw new WalletDontBelongToUserException();

            if (request.Type == TransactionType.EXPENSE.ToString())
                await ValidateExpense(request);

            var transaction = _mapper.Map<Transaction>(request); // TODO: verify possibilitty to use either factory or mapping builder pattern

            var wallet = await _walletRepository.GetAsync(x => x.Id == request.WalletId); // TODO: Verift if waller balance is updated on recurrence creation

            var recurrence = _mapper.Map<Recurrence>(transaction.Recurrence);

            RecurrenceTransactionService.GenerateTransactionsForRecurrence(recurrence, transaction, wallet);

            await _recurrenceRepository.CreateAsync(recurrence);
            _walletRepository.Update(wallet);

            await _unitOfWork.Commit();
        }

        private async Task Validate(CreateTransactionRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }

        private async Task ValidateExpense(CreateTransactionRequest request)
        {
            if (request.IsCompleted)
            {
                var balance = await _walletRepository.GetWalletBalance(request.WalletId);

                if (request.Amount > balance)
                    throw new InsufficientFundsException();
            }
        }
    }
}
