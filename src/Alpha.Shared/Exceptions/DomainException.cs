namespace Alpha.Shared.Exceptions;

public class DomainException : Exception
{
    protected DomainException(
        string message,
        int errorCode,
        IDictionary<string, object> errorData) : base(message)
    {
        ErrorCode = errorCode;
        ErrorData = errorData;
    }

    public int ErrorCode { get; }

    public IDictionary<string, object> ErrorData { get; }
}
