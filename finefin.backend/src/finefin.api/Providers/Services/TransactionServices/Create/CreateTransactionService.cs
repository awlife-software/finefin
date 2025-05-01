using AutoMapper;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Models.Enums;
using finefin.api.Providers.Validation.Transaction.Interfaces;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Providers.Services.TransactionServices.Create
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

            var transaction = _mapper.Map<Transaction>(request);
            var wallet = await _walletRepository.GetAsync(x => x.Id == request.WalletId);

            var recurrence = await _recurrenceRepository.CreateAndGetAsync(_mapper.Map<Recurrence>(transaction.Recurrence));

            transaction.RecurrenceId = recurrence.Id;

            for(var i = 1; i <= transaction.Recurrence!.Occurrences; i++)
            {
                if(i > 1)
                {
                    transaction.IsCompleted = false;
                    await HandleRecurrence(transaction, i);
                }
                else
                {
                    await _transactionRepository.CreateAsync(transaction);
                    HandleBalance(wallet, transaction);
                    await _unitOfWork.Commit();
                }
            }
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

        private async Task HandleRecurrence(Transaction transaction, int index)
        {
            var value = index - 1;

            if (transaction.Recurrence!.Type == RecurrenceType.DAYLI.ToString())
                transaction.DueDate = transaction.DueDate.AddDays(value);

            if (transaction.Recurrence!.Type == RecurrenceType.WEEKLY.ToString())
                transaction.DueDate = transaction.DueDate.AddDays(value * 7);

            if (transaction.Recurrence!.Type == RecurrenceType.MONTHLY.ToString())
                transaction.DueDate = transaction.DueDate.AddMonths(value);

            if (transaction.Recurrence!.Type == RecurrenceType.YEARLY.ToString())
                transaction.DueDate = transaction.DueDate.AddYears(value);


            transaction.Id = Guid.NewGuid();
            await _transactionRepository.CreateAsync(transaction);
            await _unitOfWork.Commit();
        }

        private void HandleBalance(Wallet wallet, Transaction transaction)
        {
            if (transaction.IsCompleted)
            {
                if (transaction.Type == TransactionType.INCOME.ToString())
                    wallet.Balance += transaction.Amount;
                else
                    wallet.Balance -= transaction.Amount;

                _walletRepository.Update(wallet);
            }
        }
    }
}
