using AutoMapper;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Http.Requests;
using finefin.api.Providers.Validation.Wallet.Interfaces;
using finefin.test._03._Builders.Entities;
using finefin.test._03._Builders.Requests;
using Moq;
using valet.lib.Core.Domain.Interfaces;
using FluentValidation.Results;
using finefin.api.Models.Entities;
using finefin.api.Providers.Services.WalletServices.Create;
using finefin.api.Exceptions;

namespace finefin.test._01._UnitTests.Providers.Services.WalletServices.Create
{
    public class CreateWalletServiceTest
    {
        private readonly Mock<ICreateWalletValidation> _walletValidation;
        private readonly Mock<IWalletRepository> _walletRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;

        public CreateWalletServiceTest()
        {
            _walletValidation = new();
            _walletRepository = new();
            _unitOfWork = new();
            _mapper = new();
        }

        [Fact]
        public async Task Should_Create_Wallet()
        {
            var request = CreateWalletRequestBuilder.Build();
            var wallet = WalletBuilder.Build();

            _walletValidation.Setup(x => x.ValidateAsync(It.IsAny<CreateWalletRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _mapper.Setup(x => x.Map<Wallet>(It.IsAny<CreateWalletRequest>())).Returns(wallet);

            var service = CreateService();

            await service.CreateWallet(wallet.UserId.ToString(), request);

            _walletRepository.Verify(x => x.CreateAsync(It.IsAny<Wallet>()), Times.Once);
            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Invalid_Type()
        {
            var request = CreateWalletRequestBuilder.Build();
            request.Type = "INVALID";

            _walletValidation.Setup(x => x.ValidateAsync(It.IsAny<CreateWalletRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult {Errors = [new ValidationFailure {ErrorMessage = RSC.ResourceMessageException.WALLET_TYPE_INVALID}]});

            var service = CreateService();

            var act = async () => await service.CreateWallet(Guid.NewGuid().ToString(), request);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);

            Assert.Single(exception.ErrorMessages);
            Assert.Equal(RSC.ResourceMessageException.WALLET_TYPE_INVALID, exception.ErrorMessages.First());

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
            _walletRepository.Verify(x => x.CreateAsync(It.IsAny<Wallet>()), Times.Never);
            _walletValidation.Verify(x => x.ValidateAsync(It.IsAny<CreateWalletRequest>(), It.IsAny<CancellationToken>()), Times.Once);    
        }

        public CreateWalletService CreateService() => new(_walletValidation.Object, _walletRepository.Object, _unitOfWork.Object, _mapper.Object);
    }
}
