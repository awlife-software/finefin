using finefin.Shared.Communication.Requests.User;
using FluentValidation;

namespace finefin.Application.Services.UserServices.Register.Validator
{
    public interface IUserRegisterValidation : IValidator<RegisterUserRequest>
    {
    }
}
