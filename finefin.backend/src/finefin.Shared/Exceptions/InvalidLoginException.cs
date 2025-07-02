using RSC;
using System.Net;
using valet.lib.Core.Exception;

namespace finefin.Shared.Exceptions
{
    public class InvalidLoginException : BaseException
    {
        public InvalidLoginException() : base(ResourceMessageException.LOGIN_INVALID) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
