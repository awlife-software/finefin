using finefin_test._03._Builders.Requests;
using RSC;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace finefin_test._02._IntegrationTests.Controllers.User
{
    public class UserRegisterTest(CustomWebApplicationFactory factory) : ApplicationClassFixture(factory)
    {
        [Fact]
        public async Task Should_Register_User()
        {
            var request = RegisterUserRequestBuilder.Build();

            var result = await Post("user/register", request);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Name_Empty()
        {
            var request = RegisterUserRequestBuilder.Build();
            request.FirstName = string.Empty;

            var result = await Post("user/register", request);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            await using var responseBody = await result.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceMessageException.ResourceManager.GetString("FIRSTNAME_EMPTY");

            Assert.Single(errors);
            Assert.Contains(expectedMessage!, errors.First().ToString());
        }
    }
}
