using Bogus;

namespace finefin_test._03._Builders.Providers
{
    public static class PasswordBuilder
    {
        public static string Build(int length = 10)
        {
            var half = (length - 1) / 2;
            var faker = new Faker();
            var password = faker.Internet.Password(length - 1);

            return password.Substring(0, half).ToUpper() + "#" + password.Substring(half).ToLower();
        }
    }
}
