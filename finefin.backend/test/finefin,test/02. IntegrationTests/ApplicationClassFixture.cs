using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using System.Net.Http.Json;

namespace finefin.test._02._IntegrationTests
{
    public class ApplicationClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient httpClient;
        public ApplicationClassFixture(CustomWebApplicationFactory factory) => this.httpClient = factory.CreateClient();

        protected async Task<HttpResponseMessage> Post(string method, object request, string token = "")
        {
            var message = new HttpRequestMessage(HttpMethod.Post, method)
            {
                Content = JsonContent.Create(request)
            };

            if (!string.IsNullOrWhiteSpace(token))
                message.Headers.Add("Authorization", $"Bearer {token}");

            return await this.httpClient.SendAsync(message);
        }

        protected async Task<string> LoginAndGetToken(string email, string password)
        {
            var request = new UserLoginRequest { Email = email, Password = password };

            var result = await Post("user/login", request);

            var content = await result.Content.ReadFromJsonAsync<UserLoginResponse>();

            return content?.Token ?? throw new InvalidOperationException("Login failed.");
        }
    }
}
