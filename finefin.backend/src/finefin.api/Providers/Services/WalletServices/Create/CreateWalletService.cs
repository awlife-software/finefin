
using AutoMapper;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Providers.Validation.Wallet.Interfaces;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Providers.Services.WalletServices.Create
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

        public async Task CreateWallet(string userId, CreateWalletRequest request)
        {
            await ValidateAsync(request);

            var wallet = _mapper.Map<Wallet>(request);

            wallet.UserId = Guid.Parse(userId);

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
