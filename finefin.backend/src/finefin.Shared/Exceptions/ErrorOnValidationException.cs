using System.Net;
using valet.lib.Core.Exception;

namespace finefin.Shared.Exceptions
{
    public class ErrorOnValidationException : BaseException
    {
        public IList<string> ErrorMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            ErrorMessages = errorMessages;
        }

        public override IList<string> GetErrorMessages() => ErrorMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
