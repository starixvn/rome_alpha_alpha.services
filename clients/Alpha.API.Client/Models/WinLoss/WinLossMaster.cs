namespace Alpha.API.Client.Models.WinLoss;

public record WinLossMaster(
    decimal Turnover,
    decimal GrossComm,
    decimal MasterTotal,
    decimal SuperWinLoss,
    decimal SuperComm,
    decimal SuperTotal,
    decimal CompanyWinLoss) : WinLossBase(Turnover, GrossComm, CompanyWinLoss);