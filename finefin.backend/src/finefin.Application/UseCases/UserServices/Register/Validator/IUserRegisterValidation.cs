using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.UserServices.Register.Validator
{
    public interface IUserRegisterValidation : IValidator<RegisterUserRequest>
    {
    }
}
