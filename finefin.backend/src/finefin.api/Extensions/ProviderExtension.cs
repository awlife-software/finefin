using finefin.api.Providers.Mapper;
using finefin.api.Providers.Services.UserServices.Login;
using finefin.api.Providers.Services.UserServices.Register;
using finefin.api.Providers.Services.WalletServices.Create;
using finefin.api.Providers.Validation.User;
using finefin.api.Providers.Validation.User.Interfaces;
using finefin.api.Providers.Validation.Wallet;
using finefin.api.Providers.Validation.Wallet.Interfaces;

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
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICreateWalletService, CreateWalletService>();
        }

        private static void AddExtraProviders(this IServiceCollection services)
        {
            services.AddTransient<IUserRegisterValidation, UserRegisterValidation>();
            services.AddTransient<ILoginValidation, LoginValidation>();
            services.AddTransient<ICreateWalletValidation, CreateWalletValidation>();
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
