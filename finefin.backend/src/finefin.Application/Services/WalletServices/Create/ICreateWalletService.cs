using finefin.Shared.Communication.Requests.Wallet;

namespace finefin.Application.Services.WalletServices.Create
{
    public interface ICreateWalletService
    {
        Task Create(string userId, CreateWalletRequest request);
    }
}
