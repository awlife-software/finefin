namespace finefin.Shared.Communication.Responses.User
{
    public class UserLoginResponse
    {
        public UserLoginResponse(string token)
        {
            Token = token;
        }
        public string Token { get; set; } = string.Empty;
    }
}
