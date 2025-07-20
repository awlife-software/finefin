using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.User.Login.Validator
{
    public interface IUserLoginValidation : IValidator<UserLoginRequest>
    {
    }
}
