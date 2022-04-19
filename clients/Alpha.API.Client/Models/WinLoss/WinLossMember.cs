namespace Alpha.API.Client.Models.WinLoss;

public record WinLossMember(
    decimal Turnover,
    decimal GrossComm,
    decimal MemberWinLoss,
    decimal MemberComm,
    decimal AgentWinLoss,
    decimal AgentComm,
    decimal MemberTotal,
    decimal AgentTotal,
    decimal CompanyWinLoss) : WinLossBase(Turnover, GrossComm, CompanyWinLoss);