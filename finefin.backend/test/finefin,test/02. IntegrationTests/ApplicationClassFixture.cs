using System.Net.Http.Json;

namespace finefin_test._02._IntegrationTests
{
    public class ApplicationClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient httpClient;
        public ApplicationClassFixture(CustomWebApplicationFactory factory) => this.httpClient = factory.CreateClient();

        protected async Task<HttpResponseMessage> Post(string method, object request) => await this.httpClient.PostAsJsonAsync(method, request);
    }
}
