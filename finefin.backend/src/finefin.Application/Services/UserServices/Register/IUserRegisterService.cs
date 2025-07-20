using finefin.Shared.Communication.Requests.User;

namespace finefin.Application.Services.UserServices.Register
{
    public interface IUserRegisterService
    {
        Task RegisterUser(RegisterUserRequest request);
    }
}
