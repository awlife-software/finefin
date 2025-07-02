using System.Net;
using valet.lib.Core.Exception;

namespace finefin.Shared.Exceptions
{
    public class InvalidIdException : BaseException
    {
        public InvalidIdException() : base(RSC.ResourceMessageException.INVALID_ID) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
