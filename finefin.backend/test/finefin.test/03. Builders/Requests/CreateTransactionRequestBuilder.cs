using Bogus;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Models.Enums;

namespace finefin.test._03._Builders.Requests
{
    public static class CreateTransactionRequestBuilder
    {
        public static CreateTransactionRequest Build()
        {
            return new Faker<CreateTransactionRequest>()
                .RuleFor(x => x.Type, RandomTransactionType())
                .RuleFor(x => x.Amount, (f) => f.Finance.Amount())
                .RuleFor(x => x.IsCompleted, (f) => f.Random.Bool())
                .RuleFor(x => x.Recurrence, GetRecurrence())
                .RuleFor(x => x.WalletId, (f) => f.Random.Guid());
        }

        private static RecurrenceRequest GetRecurrence()
        {
            return new Faker<RecurrenceRequest>()
                .RuleFor(x => x.Occurrences, (f) => f.Random.Int(1, 6))
                .RuleFor(x => x.Type, (f, instance) =>
                    instance.Occurrences == 1 ? RecurrenceType.SINGLE.ToString() : RecurrenceType.MONTHLY.ToString());
        }

        private static string RandomTransactionType()
        {
            var random = new Random();
            var value = random.Next();

            if (value % 2 == 0)
                return TransactionType.INCOME.ToString();
            else
                return TransactionType.EXPENSE.ToString();
        }
    }
}
