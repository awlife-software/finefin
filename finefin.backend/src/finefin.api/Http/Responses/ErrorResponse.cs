namespace finefin.api.Http.Responses
{
    public class ErrorResponse
    {
        public IReadOnlyList<string> ErrorMessages { get; }

        public ErrorResponse(IEnumerable<string> errorMessages) =>
            ErrorMessages = errorMessages.ToList().AsReadOnly();

        public ErrorResponse(string errorMessage) =>
            ErrorMessages = new List<string> { errorMessage }.AsReadOnly();

        public ErrorResponse(Exception ex) =>
            ErrorMessages = new List<string> { ex.Message }.AsReadOnly();
    }
}
