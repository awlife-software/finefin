using finefin.Application.UseCases.Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using valet.lib.Auth.Service.Token.Middlewares;
using valet.lib.Core.Exception.Response;

namespace finefin.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        [ValidateUser]
        [HttpGet("CurrentMonthSummary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentMonthSummary([FromServices] ISummary service)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            return Ok(await service.GetSummary(userId!, DateTime.UtcNow));
        }

        [ValidateUser]
        [HttpGet("SpecificMonthSummary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSpecificMonthSummary([FromServices] ISummary service, [FromQuery] DateTime date)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

            return Ok(await service.GetSummary(userId!, date));
        }
    }
}
