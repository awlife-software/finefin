using finefin.api.Http.Requests;
using FluentValidation;

namespace finefin.api.Providers.Validation.User.Interfaces
{
    public interface ILoginValidation : IValidator<UserLoginRequest>
    {
    }
}
