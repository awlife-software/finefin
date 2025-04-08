using System.Net;
using valet.lib.Core.Exception;

namespace finefin.api.Exceptions
{
    public class ErrorOnValidationException : BaseException
    {
        public IList<string> ErrorMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            this.ErrorMessages = errorMessages;
        }

        public override IList<string> GetErrorMessages() => this.ErrorMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
