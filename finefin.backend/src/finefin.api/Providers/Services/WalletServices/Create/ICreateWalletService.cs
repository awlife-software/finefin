using finefin.api.Http.Requests;

namespace finefin.api.Providers.Services.WalletServices.Create
{
    public interface ICreateWalletService
    {
        Task CreateWallet(string userId, CreateWalletRequest request);
    }
}
