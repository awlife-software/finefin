using AutoMapper;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Providers.Security.Interfaces;
using finefin.api.Providers.Validation.User.Interfaces;

namespace finefin.api.Providers.Services.UserServices.Register
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUserRegisterValidation _userRegisterValidation;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserRegisterService(IUserRegisterValidation userRegisterValidation, IMapper mapper, IPasswordHasher passwordHasher, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _userRegisterValidation = userRegisterValidation;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task RegisterUser(RegisterUserRequest request)
        {
            await ValidateAsync(request);

            var user = _mapper.Map<User>(request);
            user.SetPassword(request.Password, _passwordHasher);

            var role = await RoleHandler();

            user.UserRoles.Add(new UserRole { Role = role });

            await _userRepository.CreateAsync(user);

            await _unitOfWork.Commit();
        }

        private async Task<Role> RoleHandler()
        {
            if (!_roleRepository.RoleExistsAsync("user").GetAwaiter().GetResult())
            {
                await _roleRepository.CreateAsync(new Role { Name = "user" });
                await _unitOfWork.Commit();
            }

            return await _roleRepository.GetAsync(x => x.Name.Equals("user"), false);
        }

        private async Task ValidateAsync(RegisterUserRequest request)
        {
            var result = await _userRegisterValidation.ValidateAsync(request);

            if (!result.IsValid)
            {
                var erros = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erros);
            }
        }
    }
}
