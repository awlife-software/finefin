using finefin.Application.Services.WalletServices.Create;
using finefin.Shared.Communication.Requests.Wallet;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using valet.lib.Auth.Service.Token.Middlewares;
using valet.lib.Core.Exception.Response;

namespace finefin.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        [ValidateUser]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateWallet([FromServices] ICreateWalletService service, [FromBody] CreateWalletRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.Create(userId!, request);

            return Created(string.Empty, null);
        }
    }
}
