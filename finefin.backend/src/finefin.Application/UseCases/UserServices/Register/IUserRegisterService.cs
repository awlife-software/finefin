using finefin.Shared.Communication.Requests;

namespace finefin.Application.UseCases.UserServices.Register
{
    public interface IUserRegisterService
    {
        Task RegisterUser(RegisterUserRequest request);
    }
}
