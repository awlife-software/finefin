using finefin.api.Http.Requests;
using finefin.api.Providers.Services.WalletServices.Create;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using valet.lib.Auth.Service.Token.Middlewares;

namespace finefin.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        [ValidateUser]
        [HttpPost("create")]
        public async Task<IActionResult> CreateWallet([FromServices] ICreateWalletService service, [FromBody] CreateWalletRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.CreateWallet(userId!, request);

            return Created();
        }
    }
}
