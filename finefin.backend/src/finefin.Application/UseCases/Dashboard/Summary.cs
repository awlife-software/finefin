using finefin.Application.UseCases.Dashboard.Interfaces;
using finefin.Domain.Interfaces.Repositories;
using finefin.Shared.Communication.Responses;

namespace finefin.Application.UseCases.Dashboard
{
    public class Summary : ISummary
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public Summary(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<SummaryResponse> GetSummary(string userId, DateTime date)
        {
            var currentMonthIncomes = await _transactionRepository.GetMonthTotalIncomes(Guid.Parse(userId), date);
            var currentMonthExpenses = await _transactionRepository.GetMonthTotalExpenses(Guid.Parse(userId), date);

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
