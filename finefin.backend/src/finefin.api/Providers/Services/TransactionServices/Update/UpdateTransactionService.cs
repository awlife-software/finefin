using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Providers.Services.TransactionServices.Update
{
    public class UpdateTransactionService : IUpdateTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;

        public UpdateTransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _walletRepository = walletRepository;
        }

        public async Task CompleteTransaction(string userId, string transactionId)
        {
            var transaction = await _transactionRepository.GetAsync(x => x.Id.ToString() == transactionId) 
                ?? throw new InvalidIdException();

            var isValid = await _walletRepository.WalletBelongsToUser(transaction.WalletId, Guid.Parse(userId));

            if (!isValid)
                throw new WalletDontBelongToUserException();           

            transaction.IsCompleted = true;

            _transactionRepository.Update(transaction);
            await _unitOfWork.Commit();
        }

        public async Task UpdateTransaction(string userId, UpdateTransactionRequest request)
        {

        }
    }
}
