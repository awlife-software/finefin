using finefin.Shared.Communication.Requests;

namespace finefin.Application.Providers.Services.UserServices.Register
{
    public interface IUserRegisterService
    {
        Task RegisterUser(RegisterUserRequest request);
    }
}
