using Bogus;
using finefin.api.Models.Entities;
using finefin.test._03._Builders.Providers;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Service.Hash;

namespace finefin.test._03._Builders.Entities
{
    public class UserBuilder
    {
        public static (LocalUser user, string password) Build()
        {
            var passwordHasher = new PasswordHasher();
            var password = PasswordBuilder.Build();

            var user = new Faker<LocalUser>()
                .RuleFor(x => x.FirstName, (f) => f.Person.FirstName)
                .RuleFor(x => x.LastName, (f) => f.Person.LastName)
                .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.FirstName, x.LastName))
                .RuleFor(x => x.Password, (f) => passwordHasher.HashPassword(password));

            return (user, password);
        }
    }
}
