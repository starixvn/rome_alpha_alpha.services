namespace Alpha.API.Client.Models;

public sealed record APIError(int Code, string Message, IDictionary<string, object> Data);
