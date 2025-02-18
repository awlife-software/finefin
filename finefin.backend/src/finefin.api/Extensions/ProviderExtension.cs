using finefin.api.Providers.Mapper;
using finefin.api.Providers.Security;
using finefin.api.Providers.Security.Interfaces;
using finefin.api.Providers.Services.UserServices.Register;
using finefin.api.Providers.Validation.User;
using finefin.api.Providers.Validation.User.Interfaces;

namespace finefin.api.Extensions
{
    public static class ProviderExtension
    {
        public static void AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddAutoMapper();
            services.AddExtraProviders();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRegisterService, UserRegisterService>();
        }

        private static void AddExtraProviders(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IUserRegisterValidation, UserRegisterValidation>();

        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new MappingConfig());
            }).CreateMapper());
        }
    }
}
