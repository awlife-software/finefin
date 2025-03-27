using RSC;

namespace finefin.api.Exceptions
{
    public class InvalidLoginException : AppBaseException
    {
        public InvalidLoginException() : base(ResourceMessageException.LOGIN_INVALID) { }
    }
}
