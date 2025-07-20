using finefin.Shared.Communication.Requests.User;
using FluentValidation;

namespace finefin.Application.Services.UserServices.Login.Validator
{
    public interface IUserLoginValidation : IValidator<UserLoginRequest>
    {
    }
}
