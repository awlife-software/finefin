using finefin.api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using valet.lib.Core.Exception;
using valet.lib.Core.Exception.Response;

namespace finefin.api.Http.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseException)
                HandleProjectException(context);
            else
                HandleUnknowException(context);
            
        }

        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException validationException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ErrorResponse(validationException.ErrorMessages));
            }

            if (context.Exception is InvalidLoginException invalidLoginException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ErrorResponse(invalidLoginException.Message));
            }
        }

        private void HandleUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ErrorResponse(RSC.ResourceMessageException.UNKNOW_ERROR));
        }
    }
}
