using clauth.lib.Core.Entities;
using clauth.lib.Data;
using finefin.api.Data.Mappings;
using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace finefin.api.Data
{
    public class AppDbContext(DbContextOptions options) : ClauthDbContext(options)
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Recurrency> Recurrencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}
