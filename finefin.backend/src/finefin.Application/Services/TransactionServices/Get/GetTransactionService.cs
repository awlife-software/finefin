using AutoMapper;
using finefin.Domain.Interfaces.Repositories;
using finefin.Shared.Communication.Responses.Transaction;
using finefin.Shared.Exceptions;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Application.Services.TransactionServices.Get
{
    public class GetTransactionService : IGetTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWalletRepository _walletRepository;

        public GetTransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IMapper mapper, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _walletRepository = walletRepository;
        }

        public async Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForUser(string userId)
        {
            var transactions = await _transactionRepository.GetAllPendingTransactionsForUserAsync(Guid.Parse(userId));

            var list = _mapper.Map<List<PendingTransactionResponse>>(transactions);

            return list;
        }

        public async Task<List<PendingTransactionResponse>> GetAllPendingTransactionsForWallet(string walletId, string userId)
        {
            var isValid = await _walletRepository.WalletBelongsToUser(Guid.Parse(walletId), Guid.Parse(userId));

            if (!isValid)
                throw new WalletDontBelongToUserException();

            var transactions = await _transactionRepository.GetAllPendingTransactionsForWalletAsync(Guid.Parse(walletId));

            var list = _mapper.Map<List<PendingTransactionResponse>>(transactions);

            return list;
        }
    }
}
