using finefin.Application.Providers.Validation.User.Interfaces;
using finefin.Shared.Communication.Requests;
using FluentValidation;
using valet.lib.Auth.Domain.Interfaces.Repositories;

namespace finefin.Application.Providers.Validation.User
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
