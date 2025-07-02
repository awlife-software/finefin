using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.Providers.Validation.User.Interfaces
{
    public interface IUserRegisterValidation : IValidator<RegisterUserRequest>
    {
    }
}
