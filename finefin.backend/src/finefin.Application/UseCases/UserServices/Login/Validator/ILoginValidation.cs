using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.UseCases.UserServices.Login.Validator
{
    public interface ILoginValidation : IValidator<UserLoginRequest>
    {
    }
}
