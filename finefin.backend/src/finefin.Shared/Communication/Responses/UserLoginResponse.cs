namespace finefin.Shared.Communication.Responses
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
