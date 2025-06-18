using finefin.api.Models.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Data.Repositories.Interfaces
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<bool> WalletBelongsToUser(Guid walletId, Guid userId);
        Task<decimal> GetWalletBalance(Guid walletId);
        Task<decimal> GetTotalBalanceForUser(Guid userId);
    }
}
