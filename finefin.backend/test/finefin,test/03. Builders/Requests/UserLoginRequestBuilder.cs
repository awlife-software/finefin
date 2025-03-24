using Bogus;
using finefin.api.Http.Requests;

namespace finefin_test._03._Builders.Requests
{
    public static class UserLoginRequestBuilder
    {
        public static UserLoginRequest Build(int passwordLength = 10)
        {
            var half = (passwordLength - 1) / 2;
            var faker = new Faker();
            var password = faker.Internet.Password(passwordLength - 1);
            return new Faker<UserLoginRequest>()
                .RuleFor(x => x.Email, (f) => f.Internet.Email())
                .RuleFor(x => x.Password, password.Substring(0, half).ToUpper() + "#" + password.Substring(half).ToLower());
        }
    }
}
