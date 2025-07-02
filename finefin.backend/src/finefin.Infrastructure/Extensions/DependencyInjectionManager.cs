using finefin.Domain.Interfaces.Repositories;
using finefin.Infrastructure.Data;
using finefin.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace finefin.Infrastructure.Extensions
{
    public static class DependencyInjectionManager
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddRepositories();

            if (config.GetValue<bool>("InMemoryTest"))
                return;

            services.AddDatabase(config);
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Default"));
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IRecurrenceRepository, RecurrenceRepository>();
        }
    }
}
