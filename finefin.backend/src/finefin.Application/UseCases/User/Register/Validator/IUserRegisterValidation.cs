using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.User.Register.Validator
{
    public interface IUserRegisterValidation : IValidator<RegisterUserRequest>
    {
    }
}
