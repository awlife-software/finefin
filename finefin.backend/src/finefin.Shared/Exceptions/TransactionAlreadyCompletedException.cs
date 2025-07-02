using RSC;
using System.Net;
using valet.lib.Core.Exception;

namespace finefin.Shared.Exceptions
{
    public class TransactionAlreadyCompletedException : BaseException
    {
        public TransactionAlreadyCompletedException() : base(ResourceMessageException.TRANSACTION_ALREADY_COMPLETED) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
