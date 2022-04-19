using Alpha.Shared.Exceptions;

namespace Alpha.Shared.Models;

public sealed record APIError
{
    public APIError(DomainException exception)
        : this(exception.ErrorCode, exception.Message, exception.ErrorData)
    {
    }

    public APIError(int code, string message, IDictionary<string, object> data = default!)
    {
        Code = code;
        Message = message;
        Data = data;
    }

    public int Code { get; }

    public string Message { get; }

    public IDictionary<string, object> Data { get; }
}
