namespace Alpha.API.Client.Models.Outstanding;

public record OutstandingEntity(int Id, string Name, decimal MemberOutstanding, decimal Outstanding, decimal? ToWin, decimal? RiskAmount);
