using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using valet.lib.Core.Data.Repositories;

namespace finefin.Infrastructure.Data.Repositories
{
    public class WalletRepository(AppDbContext db) :  Repository<Wallet>(db), IWalletRepository
    {
        public async Task<decimal> GetWalletBalance(Guid walletId) => (await dbSet.FirstAsync(x => x.Id == walletId)).Balance;

        public async Task<bool> WalletBelongsToUser(Guid walletId, Guid userId) => await dbSet.AnyAsync(x => x.Id == walletId && x.UserId == userId);

        public async Task<decimal> GetTotalBalanceForUser(Guid userId) => await dbSet.Where(x => x.UserId.Equals(userId)).SumAsync(x => x.Balance);
        
    }
}
