using finefin.Application.Providers.Mapper;
using finefin.Application.Services.DashboardServices;
using finefin.Application.Services.DashboardServices.Interfaces;
using finefin.Application.Services.TransactionServices.Create;
using finefin.Application.Services.TransactionServices.Create.Validator;
using finefin.Application.Services.TransactionServices.Get;
using finefin.Application.Services.TransactionServices.Update;
using finefin.Application.Services.TransactionServices.Update.Validator;
using finefin.Application.Services.UserServices.Login;
using finefin.Application.Services.UserServices.Login.Validator;
using finefin.Application.Services.UserServices.Register;
using finefin.Application.Services.UserServices.Register.Validator;
using finefin.Application.Services.WalletServices.Create;
using finefin.Application.Services.WalletServices.Create.Validator;
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
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<ICreateWalletService, CreateWalletService>();
            services.AddScoped<ICreateTransactionService, CreateTransactionService>();
            services.AddScoped<IGetTransactionService, GetTransactionService>();
            services.AddScoped<IUpdateTransactionService, UpdateTransactionService>();
            services.AddScoped<ISummaryService, SummaryService>();
        }

        private static void AddExtraProviders(this IServiceCollection services)
        {
            services.AddTransient<IUserRegisterValidation, UserRegisterValidation>();
            services.AddTransient<IUserLoginValidation, UserLoginValidation>();
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
