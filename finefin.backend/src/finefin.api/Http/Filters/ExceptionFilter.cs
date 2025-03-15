using finefin.api.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace finefin.api.Http.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppBaseException)
                HandleProjectException(context);
            else
                HandleUnknowException(context);
            
        }

        private void HandleProjectException(ExceptionContext context)
        {

        }

        private void HandleUnknowException(ExceptionContext context)
        {

        }
    }
}
