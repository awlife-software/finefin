using finefin.Shared.Communication.Requests;

namespace finefin.Application.UseCases.Wallet.Create
{
    public interface ICreateWallet
    {
        Task CreateWallet(string userId, CreateWalletRequest request);
    }
}
