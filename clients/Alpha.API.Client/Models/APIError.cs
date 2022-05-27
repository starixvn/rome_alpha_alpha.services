namespace Alpha.API.Client.Models;

public sealed record ApiError(int Code, string Message, IDictionary<string, object> Data);
