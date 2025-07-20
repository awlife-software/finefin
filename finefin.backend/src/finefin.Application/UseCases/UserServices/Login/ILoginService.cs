using finefin.Shared.Communication.Requests;
using finefin.Shared.Communication.Responses;

namespace finefin.Application.UseCases.UserServices.Login
{
    public interface ILoginService
    {
        Task<UserLoginResponse> Login(UserLoginRequest request);
    }
}
