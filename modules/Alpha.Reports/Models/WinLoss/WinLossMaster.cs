namespace Alpha.Reports.Models.WinLoss;

public record WinLossMaster(
    decimal Turnover,
    decimal GrossComm,
    decimal AgentTotal,
    decimal MasterWinLoss,
    decimal MasterComm) : WinLossBase(Turnover,GrossComm )
{
    public decimal MasterTotal => MasterWinLoss + MasterComm;

    public decimal CompanyWinLoss => -(AgentTotal + MasterTotal);
}
