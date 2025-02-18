namespace finefin.api.Providers.Security.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string givenPassword, string storedPassword);
    }
}
