using Bogus;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;
using finefin.api.Models.Enums;

namespace finefin.test._03._Builders.Entities
{
    public static class TransactionBuilder
    {
        public static Transaction Build()
        {
            return new Faker<Transaction>()
                .RuleFor(x => x.Type, RandomTransactionType())
                .RuleFor(x => x.Amount, (f) => f.Finance.Amount())
                .RuleFor(x => x.IsCompleted, (f) => f.Random.Bool())
                .RuleFor(x => x.Recurrence, GetRecurrence())
                .RuleFor(x => x.WalletId, (f) => f.Random.Guid());
        }

        private static Recurrence GetRecurrence()
        {
            return new Faker<Recurrence>()
                .RuleFor(x => x.Id, (f) => f.Random.Guid())
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
