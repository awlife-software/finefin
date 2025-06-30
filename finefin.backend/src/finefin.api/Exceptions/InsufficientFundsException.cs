using System.Net;
using valet.lib.Core.Exception;

namespace finefin.api.Exceptions
{
    public class InsufficientFundsException : BaseException
    {
        public InsufficientFundsException() : base(RSC.ResourceMessageException.INSUFFICIENT_FUNDS) { }
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
