using finefin.api.Providers.Services.UserServices.Login;
using finefin.api.Providers.Validation.User.Interfaces;
using finefin_test._03._Builders.Requests;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Auth.Domain.Interfaces;
using Moq;
using finefin_test._03._Builders.Entities;
using finefin.api.Providers.Validation.User;
using valet.lib.Auth.Service.Hash;
using valet.lib.Auth.Service.Token;

namespace finefin_test._01._UnitTests.Providers.Services.UserServices.Login
{
    public class UserLoginServiceTest
    {
        private readonly ILoginValidation _loginValidation;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public UserLoginServiceTest()
        {
            _userRepository = new();
            _loginValidation = new LoginValidation(_userRepository.Object);
            _passwordHasher = new PasswordHasher();
            _tokenGenerator = new TokenGenerator("_>?_2n_!M9@!&^Z**v5pAD\\\",d3$ET1maIus\\7wd7'r4,2HKQo");
        }
        [Fact]
        public async Task Success()
        {
            var request = UserLoginRequestBuilder.Build();
            var user = UserBuilder.Build(request.Password);

            var service = CreateService();

            _userRepository.Setup(x => x.UserExists(It.Is<string>(x => x == request.Email))).ReturnsAsync(true);
            _userRepository.Setup(x => x.GetAsync(x => x.Email == request.Email, It.IsAny<bool>())).ReturnsAsync(user.user);

            var result = await service.Login(request);

            Assert.NotNull(result);
            Assert.StartsWith("ey", result.Token);
            _userRepository.Verify(x => x.GetAsync(y => y.Email == request.Email, It.IsAny<bool>()), Times.Once);
            _userRepository.Verify(x => x.UserExists(It.Is<string>(x => x == request.Email)), Times.Once);

        }

        public LoginService CreateService() => new(_loginValidation, _userRepository.Object, _passwordHasher, _tokenGenerator);
    }
}
