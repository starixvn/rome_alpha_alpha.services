namespace Alpha.Reports.Models.WinLoss;

public record WinLossSuper(
    decimal Turnover,
    decimal GrossComm,
    decimal MasterTotal,
    decimal SuperWinLoss,
    decimal SuperComm) : WinLossBase(Turnover,GrossComm )
{
    public decimal SuperTotal => SuperWinLoss + SuperComm;

    public decimal CompanyWinLoss => -(MasterTotal + SuperTotal);
}
