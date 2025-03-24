using Bogus;
using finefin.api.Http.Requests;

namespace finefin_test._03._Builders.Requests
{
    public static class RegisterUserRequestBuilder
    {
        public static RegisterUserRequest Build(int passwordLength = 10)
        {
            var half = (passwordLength - 1) / 2;
            var faker = new Faker();
            var password = faker.Internet.Password(passwordLength - 1);
            return new Faker<RegisterUserRequest>()
                .RuleFor(user => user.FirstName, (f) => f.Person.FirstName)
                .RuleFor(user => user.LastName, (f) => f.Person.LastName)
                .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(user => user.Password, password.Substring(0,half).ToUpper() + "#" + password.Substring(half).ToLower() );
        }
    }
}
