using AutoMapper;
using finefin.api.Data.Repositories.Interfaces;
using finefin.api.Exceptions;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Providers.Services.TransactionServices.Create;
using finefin.api.Providers.Validation.Transaction.Interfaces;
using finefin.test._03._Builders.Entities;
using finefin.test._03._Builders.Requests;
using FluentValidation.Results;
using Moq;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.test._01._UnitTests.Providers.Services.TransactionServices.Create
{
    public class CreateTransactionServiceTest
    {
        private readonly Mock<ITransactionRepository> _transactionRepository;
        private readonly Mock<IRecurrenceRepository> _recurrenceRepository;
        private readonly Mock<IWalletRepository> _walletRepository;
        private readonly Mock<ICreateTransactionValidation> _validator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;

        public CreateTransactionServiceTest()
        {
            _transactionRepository = new();
            _recurrenceRepository = new();
            _walletRepository = new();
            _validator = new();
            _unitOfWork = new();
            _mapper = new();
        }

        [Fact]
        public async Task Should_Create_Transaction()
        {
            var request = CreateTransactionRequestBuilder.Build();
            var transaction = TransactionBuilder.Build();
            var userId = Guid.NewGuid();

            _validator.Setup(x => x.ValidateAsync(It.IsAny<CreateTransactionRequest>(), new CancellationToken())).ReturnsAsync(new ValidationResult());
            _walletRepository.Setup(x => x.WalletBelongsToUser(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);
            _mapper.Setup(x => x.Map<Transaction>(It.IsAny<CreateTransactionRequest>())).Returns(transaction);
            _recurrenceRepository.Setup(x => x.CreateAndGetAsync(It.IsAny<Recurrence>())).ReturnsAsync(transaction.Recurrence!);

            var service = CreateService();

            await service.CreateTransaction(userId.ToString(), request);

            _transactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>()), Times.Exactly(transaction.Recurrence!.Occurrences));
            _unitOfWork.Verify(x => x.Commit(), Times.Exactly(transaction.Recurrence!.Occurrences));
        }

        [Fact]
        public async Task Should_Throw_Invalid_Type()
        {
            var request = CreateTransactionRequestBuilder.Build();
            var transaction = TransactionBuilder.Build();
            var userId = Guid.NewGuid();
            request.Type = "INVALID";

            _validator.Setup(x => x.ValidateAsync(It.IsAny<CreateTransactionRequest>(), new CancellationToken()))
                .ReturnsAsync(new ValidationResult { Errors = [new ValidationFailure {ErrorMessage = RSC.ResourceMessageException.TRANSACTION_TYPE_INVALID}]});

            var service = CreateService();

            var act = async () => await service.CreateTransaction(userId.ToString(), request);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);

            Assert.Single(exception.ErrorMessages);
            Assert.Equal(RSC.ResourceMessageException.TRANSACTION_TYPE_INVALID, exception.ErrorMessages.First());

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
            _transactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>()), Times.Never);
            _validator.Verify(x => x.ValidateAsync(It.IsAny<CreateTransactionRequest>(), new CancellationToken()), Times.Once);
        }

        private CreateTransactionService CreateService() => new(_transactionRepository.Object, _recurrenceRepository.Object, _walletRepository.Object, _validator.Object, _unitOfWork.Object, _mapper.Object);
    }
}
