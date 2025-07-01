using finefin.api.Http.Requests;
using RSC;
using System.Net;
using System.Text.Json;

namespace finefin.test._02._IntegrationTests.Controllers.User
{
    public class UserLoginTest : ApplicationClassFixture
    {
        private readonly string _email;
        private readonly string _password;

        public UserLoginTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _email = factory.GetEmail();
            _password = factory.GetPassword();
        }

        [Fact]
        public async Task Should_Login()
        {
            var request = new UserLoginRequest
            {
                Email = _email,
                Password = _password
            };

            var result = await Post("user/login", request);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Should_Throw_Invalid_Login()
        {
            var request = new UserLoginRequest
            {
                Email = _email,
                Password = _password.Replace("#", "*")
            };

            var result = await Post("user/login", request);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            await using var respondeBody = await result.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(respondeBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceMessageException.ResourceManager.GetString("LOGIN_INVALID");

            Assert.Single(errors);
            Assert.Contains(expectedMessage!, errors.First().ToString());
        }
    }
}
