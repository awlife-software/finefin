using finefin.api.Data;
using finefin.api.Data.Repositories;
using finefin.api.Data.Repositories.Interfaces;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
