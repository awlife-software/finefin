
using AutoMapper;
using finefin.Application.Services.WalletServices.Create.Validator;
using finefin.Domain.Entities;
using finefin.Domain.Interfaces.Repositories;
using finefin.Shared.Communication.Requests.Wallet;
using finefin.Shared.Exceptions;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Application.Services.WalletServices.Create
{
    public class CreateWalletService : ICreateWalletService
    {
        private readonly ICreateWalletValidation _walletValidation;
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreateWalletService(ICreateWalletValidation walletValidation, IWalletRepository walletRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _walletValidation = walletValidation;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(string userId, CreateWalletRequest request)
        {
            await ValidateAsync(request);

            var wallet = _mapper.Map<Domain.Entities.Wallet>(request);

            //wallet = Guid.Parse(userId);

            await _walletRepository.CreateAsync(wallet);
            await _unitOfWork.Commit();
        }

        private async Task ValidateAsync(CreateWalletRequest request)
        {
            var result = await _walletValidation.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
