using Bogus;
using finefin.api.Http.Requests;
using finefin_test._03._Builders.Providers;

namespace finefin_test._03._Builders.Requests
{
    public static class UserLoginRequestBuilder
    {
        public static UserLoginRequest Build(int passwordLength = 10)
        {
            return new Faker<UserLoginRequest>()
                .RuleFor(x => x.Email, (f) => f.Internet.Email())
                .RuleFor(x => x.Password, PasswordBuilder.Build(passwordLength));
        }
    }
}
