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
        private readonly IRecurrencyRepository _recurrencyRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICreateTransactionValidation _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTransactionService(ITransactionRepository transactionRepository, IRecurrencyRepository recurrencyRepository, IWalletRepository walletRepository, ICreateTransactionValidation validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _recurrencyRepository = recurrencyRepository;
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

            var recurrency = await _recurrencyRepository.CreateAndGetAsync(_mapper.Map<Recurrency>(transaction.Recurrency));

            transaction.RecurrencyId = recurrency.Id;

            for(var i = 1; i <= transaction.Recurrency!.Occurrences; i++)
            {
                if(i > 1)
                {
                    transaction.IsCompleted = false;
                    await HandleRecurrency(transaction, i);

                }
                else
                {
                    await _transactionRepository.CreateAsync(transaction);
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

        private async Task HandleRecurrency(Transaction transaction, int index)
        {
            var value = index - 1;

            if (transaction.Recurrency!.Type == RecurrencyType.DAYLI.ToString())
                transaction.DueDate = transaction.DueDate.AddDays(value);

            if (transaction.Recurrency!.Type == RecurrencyType.WEEKLY.ToString())
                transaction.DueDate = transaction.DueDate.AddDays(value * 7);

            if (transaction.Recurrency!.Type == RecurrencyType.MONTHLY.ToString())
                transaction.DueDate = transaction.DueDate.AddMonths(value);

            if (transaction.Recurrency!.Type == RecurrencyType.YEARLY.ToString())
                transaction.DueDate = transaction.DueDate.AddYears(value);


            transaction.Id = Guid.NewGuid();
            await _transactionRepository.CreateAsync(transaction);
            await _unitOfWork.Commit();
        }
    }
}
