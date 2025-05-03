using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.DashboardServices
{
    public class SummaryService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public SummaryService(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<SummaryResponse> GetSummary(string userId)
        {
            throw new NotImplementedException();
            var guid = Guid.Parse(userId);

            var balance = await _walletRepository.GetAllAsync(x => x.UserId.Equals(guid));
        }
    }
}
