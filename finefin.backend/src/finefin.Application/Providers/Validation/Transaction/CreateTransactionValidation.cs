using finefin.Application.Providers.Validation.Transaction.Interfaces;
using finefin.Domain.Entities.Enums;
using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.Providers.Validation.Transaction
{
    public class CreateTransactionValidation : AbstractValidator<CreateTransactionRequest>, ICreateTransactionValidation
    {
        public CreateTransactionValidation()
        {
            RuleFor(x => x.Type)
                .Must(BeAValidTransactionType)
                .WithMessage(RSC.ResourceMessageException.TRANSACTION_TYPE_INVALID);
            RuleFor(x => x.Amount)
                .GreaterThan(decimal.Zero)
                .WithMessage(RSC.ResourceMessageException.TRANSACTION_AMOUNT_INVALID);
            RuleFor(x => x.Recurrence.Type)
                .Must(BeAValidRecurrenceType)
                .WithMessage(RSC.ResourceMessageException.RECURRENCE_TYPE_INVALID);
            RuleFor(x => x.Recurrence.Occurrences)
                .LessThanOrEqualTo(100)
                .WithMessage(RSC.ResourceMessageException.RECURRENCE_OCCURRENCIES_MAX);
            RuleFor(x => x.Description)
                .MaximumLength(50)
                .WithMessage(RSC.ResourceMessageException.DESCRIPTION_MAX_LENGTH);
            RuleFor(x => x.Description)
                .MinimumLength(4)
                .WithMessage(RSC.ResourceMessageException.DESCRIPTION_MIN_LENGTH);

            When(x => x.Recurrence.Type == RecurrenceType.SINGLE.ToString(), () =>
            {
                RuleFor(x => x.Recurrence.Occurrences).Must(x => x.Equals(1)).WithMessage(RSC.ResourceMessageException.SINGLE_RECURRENCE_INVALID);
            });
            When(x => x.Recurrence.Type != RecurrenceType.SINGLE.ToString(), () =>
            {
                RuleFor(x => x.Recurrence.Occurrences).GreaterThan(1).WithMessage(RSC.ResourceMessageException.MULTIPLE_RECURRENCE_INVALID);
            });
        }


        private bool BeAValidTransactionType(string type) => Enum.TryParse<TransactionType>(type, out _);
        private bool BeAValidRecurrenceType(string type) => Enum.TryParse<RecurrenceType>(type, out _);
    }
}
