using finefin.api.Http.Requests;
using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.UserServices.Login
{
    public interface ILoginService
    {
        Task<UserLoginResponse> Login(UserLoginRequest request);
    }
}
