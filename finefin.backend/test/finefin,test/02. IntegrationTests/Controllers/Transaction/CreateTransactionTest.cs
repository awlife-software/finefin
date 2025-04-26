using finefin.api.Models.Entities;
using finefin.test._03._Builders.Requests;
using RSC;
using System.Net;
using System.Text.Json;

namespace finefin.test._02._IntegrationTests.Controllers.Transaction
{
    public class CreateTransactionTest : ApplicationClassFixture
    {
        private readonly string _email;
        private readonly string _password;
        private readonly finefin.api.Models.Entities.Wallet _wallet;

        public CreateTransactionTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _email = factory.GetEmail();
            _password = factory.GetPassword();
            _wallet = factory.GetWallet();
        }

        [Fact]
        public async Task Should_Create_Transaction()
        {
            var request = CreateTransactionRequestBuilder.Build();
            request.WalletId = _wallet.Id;

            var token = await LoginAndGetToken(_email, _password);

            var result = await Post("transaction/create", request, token);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Should_Throw_Invalid_Type()
        {
            var request = CreateTransactionRequestBuilder.Build();
            request.WalletId = _wallet.Id;
            request.Type = "INVALID";

            var token = await LoginAndGetToken(_email, _password);

            var result = await Post("transaction/create", request, token);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            await using var respondeBody = await result.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(respondeBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceMessageException.ResourceManager.GetString("TRANSACTION_TYPE_INVALID");

            Assert.Single(errors);
            Assert.Contains(expectedMessage!, errors.First().ToString());
        }
    }
}
