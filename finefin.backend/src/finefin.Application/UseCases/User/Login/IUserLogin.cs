using finefin.Shared.Communication.Requests;
using finefin.Shared.Communication.Responses;

namespace finefin.Application.UseCases.User.Login
{
    public interface IUserLogin
    {
        Task<UserLoginResponse> Login(UserLoginRequest request);
    }
}
