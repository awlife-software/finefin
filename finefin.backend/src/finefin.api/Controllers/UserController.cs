using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using finefin.api.Providers.Services.UserServices.Register;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace finefin.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromServices] IUserRegisterService service, [FromBody] RegisterUserRequest request)
        {
            await service.RegisterUser(request);

            return Created(string.Empty, null);
        }
    }
}
