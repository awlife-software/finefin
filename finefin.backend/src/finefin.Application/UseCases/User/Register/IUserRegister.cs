using finefin.Shared.Communication.Requests;

namespace finefin.Application.UseCases.User.Register
{
    public interface IUserRegister
    {
        Task RegisterUser(RegisterUserRequest request);
    }
}
