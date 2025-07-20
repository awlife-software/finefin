using finefin.Application.UseCases.User.Login;
using finefin.Application.UseCases.User.Register;
using finefin.Shared.Communication.Requests;
using finefin.Shared.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using valet.lib.Core.Exception.Response;

namespace finefin.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromServices] IUserRegister service, [FromBody] RegisterUserRequest request)
        {
            await service.RegisterUser(request);

            return Created(string.Empty, null);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromServices] IUserLogin service, [FromBody] UserLoginRequest request)
        {
            var result = await service.Login(request);

            return Ok(result);
        }
    }
}
