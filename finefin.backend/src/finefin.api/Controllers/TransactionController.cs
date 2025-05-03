using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using finefin.api.Providers.Services.TransactionServices.Create;
using finefin.api.Providers.Services.TransactionServices.Get;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using valet.lib.Auth.Service.Token.Middlewares;
using valet.lib.Core.Exception.Response;

namespace finefin.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [ValidateUser]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTransaction([FromServices] ICreateTransactionService service, [FromBody] CreateTransactionRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.CreateTransaction(userId!, request);

            return Created(string.Empty, null);
        }

        [HttpGet("pendingForUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingTransactionsForUser([FromServices] IGetTransactionService service)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            var pendingTransactions = await service.GetAllPendingTransactionsForUser(userId!);

            return Ok(pendingTransactions);
        }

        [HttpGet("pendingForWallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingTransactionsForWallet([FromServices] IGetTransactionService service, [FromQuery] string walletId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            var pendingTransactions = await service.GetAllPendingTransactionsForWallet(walletId, userId!);

            return Ok(pendingTransactions);
        }
    }
}
