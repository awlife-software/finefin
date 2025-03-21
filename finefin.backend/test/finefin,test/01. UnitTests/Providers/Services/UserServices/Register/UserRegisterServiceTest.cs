using AutoMapper;
using finefin.api.Providers.Services.UserServices.Register;
using finefin.api.Providers.Validation.User;
using finefin.api.Providers.Validation.User.Interfaces;
using finefin_test._03._Builders.Providers;
using finefin_test._03._Builders.Requests;
using Moq;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Domain.Interfaces;

namespace finefin_test._01._UnitTests.Providers.Services.UserServices.Register
{
    public class UserRegisterServiceTest
    {
        private readonly IUserRegisterValidation _userRegisterValidation;
        private readonly IMapper _mapper;
        private readonly Mock<IPasswordHasher> _passwordHasher;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRoleRepository> _roleRepository;

        public UserRegisterServiceTest()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _passwordHasher = new Mock<IPasswordHasher>();
            _mapper = MapperBuilder.Build();
            _userRegisterValidation = new UserRegisterValidation(_userRepository.Object);
        }

        [Fact]
        public async Task Success()
        {
            var request = RegisterUserRequestBuilder.Build();

            request.Password = request.Password.ToUpper() + "#" + request.Password.ToLower();

            _roleRepository.Setup(repo => repo.RoleExistsAsync("user")).ReturnsAsync(false);
            _roleRepository.Setup(repo => repo.GetAsync(x => x.Name == "user", false)).ReturnsAsync(new Role { Name = "user" });

            var service = CreateService();

            await service.RegisterUser(request);

            _unitOfWork.Verify(x => x.Commit(), Times.Exactly(2));
            _userRepository.Verify(x => x.CreateAsync(It.Is<User>(x => x.FirstName.Equals(request.FirstName))), Times.Once);
            _roleRepository.Verify(x => x.CreateAsync(It.Is<Role>(x => x.Name.Equals("user"))), Times.Once);
        }

        public UserRegisterService CreateService() => new(_userRegisterValidation, _mapper, _passwordHasher.Object, _unitOfWork.Object, _userRepository.Object, _roleRepository.Object);
    }
}
