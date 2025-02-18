using finefin.api.Providers.Security.Interfaces;

namespace finefin.api.Providers.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPass = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPass;
        }

        public bool VerifyPassword(string givenPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(givenPassword, storedPassword);
        }
    }
}
