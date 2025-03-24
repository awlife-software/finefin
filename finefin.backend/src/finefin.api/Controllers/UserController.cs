using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using finefin.api.Providers.Services.UserServices.Login;
using finefin.api.Providers.Services.UserServices.Register;
using Microsoft.AspNetCore.Mvc;

namespace finefin.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromServices] IUserRegisterService service, [FromBody] RegisterUserRequest request)
        {
            await service.RegisterUser(request);

            return Created(string.Empty, null);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromServices] ILoginService service, [FromBody] UserLoginRequest request)
        {
            var result = await service.Login(request);

            return Ok(result);
        }
    }
}
