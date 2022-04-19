namespace Alpha.API.Client.Models.WinLoss;

public record WinLossAgent(
    decimal Turnover,
    decimal GrossComm,
    decimal AgentTotal,
    decimal MasterWinLoss,
    decimal MasterComm,
    decimal MasterTotal,
    decimal CompanyWinLoss) : WinLossBase(Turnover, GrossComm, CompanyWinLoss);