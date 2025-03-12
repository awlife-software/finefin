using AutoMapper;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Providers.Validation.User.Interfaces;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Providers.Services.UserServices.Register
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUserRegisterValidation _userRegisterValidation;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        // TODO: ADICIONAR A CLAUTH A RESPONSÁBILIDADE PELO REPOSITÓRIO GENÉRICO, PELO UNITOFWORK E TODAS AS RESPONABILIDADES DE USERREPOSITORY

        public UserRegisterService(IUserRegisterValidation userRegisterValidation, IMapper mapper, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRegisterValidation = userRegisterValidation;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task RegisterUser(RegisterUserRequest request)
        {
            await ValidateAsync(request);

            var user = _mapper.Map<LocalUser>(request);
           
            user.SetHashedPassword(_passwordHasher.HashPassword(request.Password));

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
