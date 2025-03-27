using finefin.api.Http.Requests;
using finefin.api.Providers.Services.UserServices.Login;
using finefin.api.Providers.Validation.User.Interfaces;
using finefin_test._03._Builders.Entities;
using finefin_test._03._Builders.Requests;
using Moq;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using valet.lib.Auth.Domain.Entities;
using finefin.api.Exceptions;

namespace finefin_test._01._UnitTests.Providers.Services.UserServices.Login
{
    public class UserLoginServiceTest
    {
        private readonly Mock<ILoginValidation> _loginValidation;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IPasswordHasher> _passwordHasher;
        private readonly Mock<ITokenGenerator> _tokenGenerator;

        public UserLoginServiceTest()
        {
            _userRepository = new();
            _loginValidation = new();
            _passwordHasher = new();
            _tokenGenerator = new();
        }

        [Fact]
        public async Task Should__Login()
        {
            var request = UserLoginRequestBuilder.Build();
            

            var service = CreateService();

            _userRepository.Setup(x => x.UserExists(It.Is<string>(x => x == request.Email))).ReturnsAsync(true);
            _userRepository.Setup(x => x.GetAsync(x => x.Email == request.Email, It.IsAny<bool>())).ReturnsAsync(new User());
            _loginValidation.Setup(x => x.ValidateAsync(It.IsAny<UserLoginRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult()); // errors = 0 -> IsValid true
            _passwordHasher.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _tokenGenerator.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("eyfodasse");

            var result = await service.Login(request);

            Assert.NotNull(result);
            Assert.StartsWith("ey", result.Token);
            _userRepository.Verify(x => x.GetAsync(y => y.Email == request.Email, It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Invalid_Login()
        {
            var request = UserLoginRequestBuilder.Build();


            _userRepository.Setup(x => x.UserExists(It.Is<string>(x => x == request.Email))).ReturnsAsync(true);
            _userRepository.Setup(x => x.GetAsync(x => x.Email == request.Email, It.IsAny<bool>())).ReturnsAsync(new User());
            _loginValidation.Setup(x => x.ValidateAsync(It.IsAny<UserLoginRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _passwordHasher.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var service = CreateService();

            var act = async () => await service.Login(request);

            var exception = await Assert.ThrowsAsync<InvalidLoginException>(act);

            Assert.Equal(exception.Message, RSC.ResourceMessageException.LOGIN_INVALID);

            _userRepository.Verify(x => x.GetAsync(x => x.Email == request.Email, It.IsAny<bool>()), Times.Once);
        }

        public LoginService CreateService() => new(_loginValidation.Object, _userRepository.Object, _passwordHasher.Object, _tokenGenerator.Object);
    }
}
