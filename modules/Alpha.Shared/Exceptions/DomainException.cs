using System.Net;

namespace Alpha.Shared.Exceptions;

public class DomainException : Exception
{
    protected DomainException(
        string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        StatusCode = (int)statusCode;
    }

    public int StatusCode { get; }
}
