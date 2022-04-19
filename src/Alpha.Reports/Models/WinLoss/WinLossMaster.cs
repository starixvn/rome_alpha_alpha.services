namespace Alpha.Reports.Models.WinLoss;

public record WinLossMaster : WinLossBase
{
    public decimal MasterTotal { get; init; }

    public decimal SuperWinLoss { get; init; }

    public decimal SuperComm { get; init; }

    public decimal SuperTotal => SuperWinLoss + SuperComm;

    public decimal CompanyWinLoss => -(MasterTotal + SuperTotal);

    public static WinLossMaster operator+ (WinLossMaster m1, WinLossMaster m2)
        => new WinLossMaster
        {
            Turnover = m1.Turnover + m2.Turnover,
            GrossComm = m1.GrossComm + m2.GrossComm,
            MasterTotal = m1.MasterTotal + m2.MasterTotal,
            SuperWinLoss = m1.SuperWinLoss + m2.SuperWinLoss,
            SuperComm = m1.SuperComm + m2.SuperComm
        };
}
