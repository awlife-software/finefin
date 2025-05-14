using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using valet.lib.Core.Data.Repositories;

namespace finefin.api.Data.Repositories
{
    public class WalletRepository(AppDbContext db) :  Repository<Wallet>(db), IWalletRepository
    {
        public async Task<decimal> GetWalletBalance(Guid walletId) => (await dbSet.FirstAsync(x => x.Id == walletId)).Balance;

        public async Task<bool> WalletBelongsToUser(Guid walletId, Guid userId) => await dbSet.AnyAsync(x => x.Id == walletId && x.UserId == userId);
        
    }
}
