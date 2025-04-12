using System.Net;
using valet.lib.Core.Exception;

namespace finefin.api.Exceptions
{
    public class WalletDontBelongToUserException : BaseException
    {
        public WalletDontBelongToUserException() : base(RSC.ResourceMessageException.WALLET_USER_AUTHORIZATION) { }
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
