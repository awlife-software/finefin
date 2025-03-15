using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Data;

namespace finefin.api.Data
{
    public class AppDbContext(DbContextOptions options) : AuthDbContext(options)
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
