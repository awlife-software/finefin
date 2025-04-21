using finefin.test._03._Builders.Requests;
using RSC;
using System.Net;
using System.Text.Json;

namespace finefin.test._02._IntegrationTests.Controllers.Wallet
{
    public class CreateWalletTest : ApplicationClassFixture
    {
        private readonly string _email;
        private readonly string _password;

        public CreateWalletTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _email = factory.GetEmail();
            _password = factory.GetPassword();
        }

        [Fact]
        public async Task Should_Create_Wallet()
        {
            var request = CreateWalletRequestBuilder.Build();

            var token = await LoginAndGetToken(_email, _password);

            var result = await Post("wallet/create", request, token);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Should_Throw_Invalid_Type()
        {
            var request = CreateWalletRequestBuilder.Build();
            request.Type = "INVALID";

            var token = await LoginAndGetToken(_email, _password);

            var result = await Post("wallet/create", request, token);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            await using var respondeBody = await result.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(respondeBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceMessageException.ResourceManager.GetString("WALLET_TYPE_INVALID");

            Assert.Single(errors);
            Assert.Contains(expectedMessage!, errors.First().ToString());
        }

        [Fact]
        public async Task Should_Not_Authorize()
        {
            var request = CreateWalletRequestBuilder.Build();

            var result = await Post("wallet/create", request, "token");

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}
