namespace Alpha.Reports.Models.WinLoss;

public record WinLossBase
{
    public int CustId { get; init; }

    public string CustName { get; init; } = default!;

    public decimal Turnover { get; init; }

    public decimal GrossComm { get; init; }
}
