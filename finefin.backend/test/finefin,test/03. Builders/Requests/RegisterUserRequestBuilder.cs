using Bogus;
using finefin.api.Http.Requests;

namespace finefin_test._03._Builders.Requests
{
    public static class RegisterUserRequestBuilder
    {
        public static RegisterUserRequest Build(int passwordLength = 10)
        {
            return new Faker<RegisterUserRequest>()
                .RuleFor(user => user.FirstName, (f) => f.Person.FirstName)
                .RuleFor(user => user.LastName, (f) => f.Person.LastName)
                .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
        }
    }
}
