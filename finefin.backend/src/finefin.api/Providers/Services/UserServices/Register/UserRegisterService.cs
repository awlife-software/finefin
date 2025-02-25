using AutoMapper;
using clauth.lib.Core.Entities;
using clauth.lib.Core.Interfaces;
using clauth.lib.Core.Interfaces.Services;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Providers.Validation.User.Interfaces;

namespace finefin.api.Providers.Services.UserServices.Register
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUserRegisterValidation _userRegisterValidation;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleManager _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        // TODO: ADICIONAR A CLAUTH A RESPONSÁBILIDADE PELO REPOSITÓRIO GENÉRICO, PELO UNITOFWORK E TODAS AS RESPONABILIDADES DE USERREPOSITORY

        public UserRegisterService(IUserRegisterValidation userRegisterValidation, IMapper mapper, IPasswordHasher passwordHasher, IRoleManager roleManager, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _userRegisterValidation = userRegisterValidation;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task RegisterUser(RegisterUserRequest request)
        {
            await ValidateAsync(request);

            var user = _mapper.Map<User>(request);
           
            user.SetHashedPassword(_passwordHasher.HashPassword(request.Password));

            var role = await RoleHandler();

            user.UserRoles.Add(new ClauthUserRole { Role = role });

            await _userRepository.CreateAsync(user);

            await _unitOfWork.Commit();
        }

        private async Task<ClauthRole> RoleHandler()
        {
            if (!_roleManager.RoleExistsAsync("user").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ClauthRole { Name = "user" });
                await _unitOfWork.Commit();
            }

            return await _roleManager.GetAsync(x => x.Name.Equals("user"), false);
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
