using finefin.Application.Providers.Validation.User.Interfaces;
using finefin.Shared.Communication.Requests;
using finefin.Shared.Communication.Responses;
using finefin.Shared.Exceptions;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;

namespace finefin.Application.Providers.Services.UserServices.Login
{
    public class LoginService : ILoginService
    {
        private readonly ILoginValidation _loginValidation;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        
        public LoginService(ILoginValidation loginValidation, IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _loginValidation = loginValidation;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<UserLoginResponse> Login(UserLoginRequest request)
        {
            await ValidateAsync(request);

            var user = await _userRepository.GetAsync(x => x.Email == request.Email);

            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
                throw new InvalidLoginException();

            var token = _tokenGenerator.GenerateToken(user);

            return new UserLoginResponse(token);
        }

        private async Task ValidateAsync(UserLoginRequest request)
        {
            var result = await _loginValidation.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new InvalidLoginException();
            }
        }
    }
}
