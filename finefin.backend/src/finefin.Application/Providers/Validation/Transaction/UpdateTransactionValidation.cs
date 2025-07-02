using finefin.Application.Providers.Validation.Transaction.Interfaces;
using finefin.Shared.Communication.Requests;
using FluentValidation;

namespace finefin.Application.Providers.Validation.Transaction
{
    public class UpdateTransactionValidation : AbstractValidator<UpdateTransactionRequest>, IUpdateTransactionValidation
    {
        public UpdateTransactionValidation()
        {
            RuleFor(x => x.Description)
                .MaximumLength(50)
                .WithMessage(RSC.ResourceMessageException.DESCRIPTION_MAX_LENGTH);
            RuleFor(x => x.Description)
                .MinimumLength(4)
                .WithMessage(RSC.ResourceMessageException.DESCRIPTION_MIN_LENGTH);
            RuleFor(x => x.Amount)
                .GreaterThan(decimal.Zero)
                .WithMessage(RSC.ResourceMessageException.TRANSACTION_AMOUNT_INVALID);
            RuleFor(x => x.RecurrenceOption)
                .Must(BeAValidRecurrenceOption)
                .WithMessage(RSC.ResourceMessageException.RECURRENCE_TYPE_INVALID);
        }

        private bool BeAValidRecurrenceOption(string type) => Enum.TryParse<UpdateRecurrenceOption>(type, out _);
    }
}
