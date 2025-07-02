using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using valet.lib.Core.Exception;
using valet.lib.Core.Exception.Response;

namespace finefin.WebAPI.Http.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseException baseException)
                HandleProjectException(context, baseException);
            else
                HandleUnknowException(context);
            
        }

        private void HandleProjectException(ExceptionContext context, BaseException baseException)
        {
            context.HttpContext.Response.StatusCode = (int)baseException.GetStatusCode();
            context.Result = new ObjectResult(new ErrorResponse(baseException.GetErrorMessages()));
        }

        private void HandleUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ErrorResponse(RSC.ResourceMessageException.UNKNOW_ERROR));
        }
    }
}
