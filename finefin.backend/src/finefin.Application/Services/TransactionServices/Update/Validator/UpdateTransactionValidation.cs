using finefin.Shared.Communication.Requests.Transaction;
using FluentValidation;

namespace finefin.Application.Services.TransactionServices.Update.Validator
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
        }
    }
}
