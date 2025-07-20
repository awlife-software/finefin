using finefin.Shared.Communication.Requests.User;
using finefin.Shared.Communication.Responses.User;

namespace finefin.Application.Services.UserServices.Login
{
    public interface IUserLoginService
    {
        Task<UserLoginResponse> Login(UserLoginRequest request);
    }
}
