using finefin.api.Http.Requests;
using finefin.api.Models.Enums;
using finefin.api.Providers.Validation.Transaction.Interfaces;
using FluentValidation;

namespace finefin.api.Providers.Validation.Transaction
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
            RuleFor(x => x.Recurrence!.Type)
                .Must(BeAValidRecurrenceType)
                .WithMessage(RSC.ResourceMessageException.RECURRENCE_TYPE_INVALID);
            RuleFor(x => x.Recurrence!.Occurrences)
                .LessThanOrEqualTo(100)
                .WithMessage(RSC.ResourceMessageException.RECURRENCE_OCCURRENCIES_MAX);
        }


        private bool BeAValidTransactionType(string type) => Enum.TryParse<TransactionType>(type, out _);
        private bool BeAValidRecurrenceType(string type) => Enum.TryParse<RecurrenceType>(type, out _);
    }
}
