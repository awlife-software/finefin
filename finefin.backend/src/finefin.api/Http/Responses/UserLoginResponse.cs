namespace finefin.api.Http.Responses
{
    public class UserLoginResponse
    {
        public UserLoginResponse(string token)
        {
            this.Token = token;
        }
        public string Token { get; set; } = string.Empty;
    }
}
