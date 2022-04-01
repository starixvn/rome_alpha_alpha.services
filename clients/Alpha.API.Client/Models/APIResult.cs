namespace Alpha.API.Client.Models;

public record APIResult<T>(T? Data, int ErrorCode = 0, string ErrorMessage = "");
