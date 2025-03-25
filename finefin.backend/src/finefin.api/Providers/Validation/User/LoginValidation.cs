using finefin.api.Http.Requests;
using finefin.api.Providers.Validation.User.Interfaces;
using FluentValidation;
using valet.lib.Auth.Domain.Interfaces.Repositories;

namespace finefin.api.Providers.Validation.User
{
    public class LoginValidation : AbstractValidator<UserLoginRequest>, ILoginValidation
    {
        private readonly IUserRepository _userRepository;

        public LoginValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password)
                .Must(x => x.Any(char.IsUpper) &&
                x.Any(char.IsLower) &&
                x.Any(y => !char.IsLetterOrDigit(y)) &&
                x.Length >= 6);

            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email)
                .EmailAddress()
                    .WithMessage(RSC.ResourceMessageException.EMAIL_INVALID)
                .MustAsync(async (email, cancellation) => await _userRepository.UserExists(email));
            });

        }
    }
}
