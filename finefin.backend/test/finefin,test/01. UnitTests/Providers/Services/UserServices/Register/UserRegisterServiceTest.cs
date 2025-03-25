using AutoMapper;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Providers.Services.UserServices.Register;
using finefin.api.Providers.Validation.User;
using finefin.api.Providers.Validation.User.Interfaces;
using finefin_test._03._Builders.Entities;
using finefin_test._03._Builders.Providers;
using finefin_test._03._Builders.Requests;
using Moq;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Domain.Interfaces;
using FluentValidation.Results;

namespace finefin_test._01._UnitTests.Providers.Services.UserServices.Register
{
    public class UserRegisterServiceTest
    {
        private readonly Mock<IUserRegisterValidation> _userRegisterValidation;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPasswordHasher> _passwordHasher;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRoleRepository> _roleRepository;

        public UserRegisterServiceTest()
        {
            _roleRepository = new();
            _userRepository = new();
            _unitOfWork = new();
            _passwordHasher = new();
            _mapper = new();
            _userRegisterValidation = new();
        }

        [Fact]
        public async Task Should_Register_User()
        {
            var request = RegisterUserRequestBuilder.Build();
            var user = UserBuilder.Build();

            _roleRepository.Setup(repo => repo.RoleExistsAsync("user")).ReturnsAsync(false);
            _roleRepository.Setup(repo => repo.GetAsync(x => x.Name == "user", false)).ReturnsAsync(new Role { Name = "user" });
            _mapper.Setup(x => x.Map<LocalUser>(It.IsAny<RegisterUserRequest>())).Returns(user.user);
            _userRegisterValidation.Setup(x => x.ValidateAsync(It.IsAny<RegisterUserRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            var service = CreateService();

            await service.RegisterUser(request);

            _unitOfWork.Verify(x => x.Commit(), Times.Exactly(2));
            _userRepository.Verify(x => x.CreateAsync(It.Is<LocalUser>(x => x.FirstName.Equals(user.user.FirstName))), Times.Once);
            _roleRepository.Verify(x => x.CreateAsync(It.Is<Role>(x => x.Name.Equals("user"))), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Invalid_Password()
        {
            var request = RegisterUserRequestBuilder.Build(6);

            _userRegisterValidation.Setup(x => x.ValidateAsync(It.IsAny<RegisterUserRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult { Errors = [new ValidationFailure { ErrorMessage = RSC.ResourceMessageException.PASSWORD_INVALID }] });

            var service = CreateService();

            var act = async () => await service.RegisterUser(request);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);

            Assert.Single(exception.ErrorMessages);
            Assert.Equal(RSC.ResourceMessageException.PASSWORD_INVALID, exception.ErrorMessages.First());
            
            _unitOfWork.Verify(x => x.Commit(), Times.Never);
            _userRepository.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
            _roleRepository.Verify(x => x.CreateAsync(It.IsAny<Role>()), Times.Never);
        }

        public UserRegisterService CreateService() => new(_userRegisterValidation.Object, _mapper.Object, _passwordHasher.Object, _unitOfWork.Object, _userRepository.Object, _roleRepository.Object);
    }
}
