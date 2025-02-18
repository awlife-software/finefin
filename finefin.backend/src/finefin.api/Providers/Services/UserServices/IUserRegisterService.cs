using finefin.api.Http.Requests;

namespace finefin.api.Providers.Services.UserServices
{
    public interface IUserRegisterService
    {
        Task RegisterUser(RegisterUserRequest request);
    }
}
