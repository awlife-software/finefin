using finefin.Application.Providers.Mapper;
using finefin.Application.Providers.Services.TransactionServices.Update;
using finefin.Application.UseCases.Dashboard;
using finefin.Application.UseCases.Dashboard.Interfaces;
using finefin.Application.UseCases.Transaction.Create;
using finefin.Application.UseCases.Transaction.Create.Validator;
using finefin.Application.UseCases.Transaction.Search;
using finefin.Application.UseCases.Transaction.Update.Validator;
using finefin.Application.UseCases.UserServices.Login;
using finefin.Application.UseCases.UserServices.Login.Validator;
using finefin.Application.UseCases.UserServices.Register;
using finefin.Application.UseCases.UserServices.Register.Validator;
using finefin.Application.UseCases.WalletServices.Create;
using finefin.Application.UseCases.WalletServices.Create.Validator;
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
            services.AddScoped<ICreateTransaction, CreateTransaction>();
            services.AddScoped<ISearchTransaction, SearchTransaction>();
            //services.AddScoped<IUpdateTransactionService, UpdateTransactionService>();
            services.AddScoped<ISummary, Summary>();
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
