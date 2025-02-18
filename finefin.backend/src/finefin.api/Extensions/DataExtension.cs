using finefin.api.Data;
using Microsoft.EntityFrameworkCore;

namespace finefin.api.Extensions
{
    public static class DataExtension
    {
        public static void AddData(this IServiceCollection services, IConfiguration config)
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
            // TODO: ADD REPOSITORIES
        }
    }
}
