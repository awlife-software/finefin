using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Http.Responses;
using finefin.api.Providers.Services.DashboardServices.Interfaces;

namespace finefin.api.Providers.Services.DashboardServices
{
    public class SummaryService : ISummaryService
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
            var currentMonthIncomes = await _transactionRepository.GetMonthTotalIncomes(Guid.Parse(userId), DateTime.UtcNow);
            var currentMonthExpenses = await _transactionRepository.GetMonthTotalExpenses(Guid.Parse(userId), DateTime.UtcNow);

            return new SummaryResponse
            {
                TotalBalance = await _walletRepository.GetTotalBalanceForUser(Guid.Parse(userId)),
                CurrentMonthIncomes = currentMonthIncomes,
                CurrentMonthExpenses = currentMonthExpenses,
                CurrentMonthBalance = currentMonthIncomes - currentMonthExpenses
            };
        }
    }
}
