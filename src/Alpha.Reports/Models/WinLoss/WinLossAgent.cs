namespace Alpha.Reports.Models.WinLoss;

public record WinLossAgent : WinLossBase
{
    public decimal AgentTotal { get; init; }

    public decimal MasterWinLoss { get; init; }

    public decimal MasterComm { get; init; }

    public decimal MasterTotal => MasterWinLoss + MasterComm;

    public decimal CompanyWinLoss => -(AgentTotal + MasterTotal);

    public static WinLossAgent operator+ (WinLossAgent a1, WinLossAgent a2)
        => new WinLossAgent
        {
            Turnover = a1.Turnover + a2.Turnover,
            GrossComm = a1.GrossComm + a2.GrossComm,
            AgentTotal = a1.AgentTotal + a2.AgentTotal,
            MasterWinLoss = a1.MasterWinLoss + a2.MasterWinLoss,
            MasterComm = a1.MasterComm + a2.MasterComm
        };
}
