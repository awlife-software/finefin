using Microsoft.AspNetCore.Mvc.Filters;

namespace finefin.api.Http.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
