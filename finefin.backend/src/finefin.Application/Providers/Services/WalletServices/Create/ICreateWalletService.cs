using finefin.Shared.Communication.Requests;

namespace finefin.Application.Providers.Services.WalletServices.Create
{
    public interface ICreateWalletService
    {
        Task CreateWallet(string userId, CreateWalletRequest request);
    }
}
