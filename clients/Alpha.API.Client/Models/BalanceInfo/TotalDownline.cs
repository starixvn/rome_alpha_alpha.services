namespace Alpha.API.Client.Models.BalanceInfo;

public record TotalDownline(byte RoleId, int TotalActive, int TotalClosed, int TotalSuspended);