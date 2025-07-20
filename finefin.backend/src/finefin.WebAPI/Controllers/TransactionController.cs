using finefin.Application.UseCases.Transaction.Create;
using finefin.Application.UseCases.Transaction.Search;
using finefin.Application.UseCases.Transaction.Update;
using finefin.Shared.Communication.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using valet.lib.Auth.Service.Token.Middlewares;
using valet.lib.Core.Exception.Response;

namespace finefin.WebAPI.Controllers
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
        public async Task<IActionResult> CreateTransaction([FromServices] ICreateTransaction service, [FromBody] CreateTransactionRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.CreateTransaction(userId!, request);

            return Created(string.Empty, null);
        }

        [ValidateUser]
        [HttpGet("pending/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingTransactionsForUser([FromServices] ISearchTransaction service)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            return Ok(await service.GetAllPendingTransactionsForUser(userId!));
        }

        [ValidateUser]
        [HttpGet("pending/wallet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingTransactionsForWallet([FromServices] ISearchTransaction service, string id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            return Ok(await service.GetAllPendingTransactionsForWallet(id, userId!));
        }

        [ValidateUser]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateTransaction([FromServices] IUpdateTransaction service, [FromBody] UpdateTransactionRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.UpdateTransaction(userId!, request);

            return Ok();
        }

        [ValidateUser]
        [HttpPut("complete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CompleteTransaction([FromServices] IUpdateTransaction service, string id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            await service.CompleteTransaction(userId!, id);

            return Ok();
        }
    }
}
