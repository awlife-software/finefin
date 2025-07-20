using finefin.Shared.Communication.Requests;

namespace finefin.Application.UseCases.WalletServices.Create
{
    public interface ICreateWalletService
    {
        Task CreateWallet(string userId, CreateWalletRequest request);
    }
}
