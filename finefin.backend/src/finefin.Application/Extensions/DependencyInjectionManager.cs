using finefin.Application.Providers.Mapper;
using finefin.Application.Providers.Services.DashboardServices;
using finefin.Application.Providers.Services.DashboardServices.Interfaces;
using finefin.Application.Providers.Services.TransactionServices.Create;
using finefin.Application.Providers.Services.TransactionServices.Get;
using finefin.Application.Providers.Services.TransactionServices.Update;
using finefin.Application.Providers.Services.UserServices.Login;
using finefin.Application.Providers.Services.UserServices.Register;
using finefin.Application.Providers.Services.WalletServices.Create;
using finefin.Application.Providers.Validation.Transaction;
using finefin.Application.Providers.Validation.Transaction.Interfaces;
using finefin.Application.Providers.Validation.User;
using finefin.Application.Providers.Validation.User.Interfaces;
using finefin.Application.Providers.Validation.Wallet;
using finefin.Application.Providers.Validation.Wallet.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace finefin.Application.Extensions
{
    public static class DependencyInjectionManager
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<ICreateTransactionService, CreateTransactionService>();
            services.AddScoped<IGetTransactionService, GetTransactionService>();
            //services.AddScoped<IUpdateTransactionService, UpdateTransactionService>();
            services.AddScoped<ISummaryService, SummaryService>();
        }

        private static void AddExtraProviders(this IServiceCollection services)
        {
            services.AddTransient<IUserRegisterValidation, UserRegisterValidation>();
            services.AddTransient<ILoginValidation, LoginValidation>();
            services.AddTransient<ICreateWalletValidation, CreateWalletValidation>();
            services.AddTransient<ICreateTransactionValidation, CreateTransactionValidation>();
            services.AddTransient<IUpdateTransactionValidation, UpdateTransactionValidation>();
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
