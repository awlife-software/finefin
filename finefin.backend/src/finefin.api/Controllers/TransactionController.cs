using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using finefin.api.Providers.Services.TransactionServices.Create;
using finefin.api.Providers.Services.TransactionServices.Get;
using finefin.api.Providers.Services.TransactionServices.Update;
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

        [HttpGet("pending/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingTransactionsForUser([FromServices] IGetTransactionService service)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            return Ok(await service.GetAllPendingTransactionsForUser(userId!));
        }

        [HttpGet("pending/wallet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingTransactionsForWallet([FromServices] IGetTransactionService service, string id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            return Ok(await service.GetAllPendingTransactionsForWallet(id, userId!));
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateTransaction([FromServices] IUpdateTransactionService service, [FromBody] UpdateTransactionRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.UpdateTransaction(userId!, request);

            return Ok();
        }

        [HttpPut("complete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CompleteTransaction([FromServices] IUpdateTransactionService service, string id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.CompleteTransaction(userId!, id);

            return Ok();
        }
    }
}
