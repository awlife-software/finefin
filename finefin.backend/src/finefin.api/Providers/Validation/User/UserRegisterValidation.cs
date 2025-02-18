using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Http.Requests;
using finefin.api.Providers.Validation.User.Interfaces;
using finefin.api.Exceptions;
using FluentValidation;
using System.Resources;
using RSC;

namespace finefin.api.Providers.Validation.User
{
    public class UserRegisterValidation : AbstractValidator<RegisterUserRequest>, IUserRegisterValidation
    {
        private readonly IUserRepository _userRepository;

        public UserRegisterValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage(RSC.ResourceMessageException.FIRSTNAME_EMPTY);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(RSC.ResourceMessageException.LASTNAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(RSC.ResourceMessageException.EMAIL_EMPTY);

            RuleFor(x => x.Password)
                .Must(x => x.Any(char.IsUpper) &&
                x.Any(char.IsLower) &&
                x.Any(y => !char.IsLetterOrDigit(y)) &&
                x.Length >= 6)
                .WithMessage(RSC.ResourceMessageException.PASSWORD_INVALID);

            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email)
                .EmailAddress()
                    .WithMessage(RSC.ResourceMessageException.EMAIL_INVALID)
                .MustAsync(async (email, cancellation) => !await _userRepository.UserExists(email))
                    .WithMessage(RSC.ResourceMessageException.EMAIL_EXISTS);
            });

            When(x => !string.IsNullOrEmpty(x.FirstName), () =>
            {
                RuleFor(x => x.FirstName).Length(2, 30).WithMessage(RSC.ResourceMessageException.FIRSTNAME_LENGTH);
            });
            When(x => !string.IsNullOrEmpty(x.LastName), () =>
            {
                RuleFor(x => x.LastName).Length(2, 30).WithMessage(RSC.ResourceMessageException.LASTNAME_LENGTH);
            });

        }
    }
}
