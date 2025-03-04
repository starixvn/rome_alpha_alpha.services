namespace Alpha.Reports.Models.Outstanding;

public record OutstandingModel(int Id, string Name, decimal MemberOutstanding, decimal Outstanding, decimal? ToWin, decimal? RiskAmount);
