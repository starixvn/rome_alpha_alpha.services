namespace Alpha.Reports.Models.BalanceInfo;

public record TopBalanceReport
{
    public TopBalanceReport()
        : this(Enumerable.Empty<TopBalance>(), new TopBalance())
    {
    }

    public TopBalanceReport(
        IEnumerable<TopBalance> topBalances,
        TopBalance total,
        int hiddenDownlinesCount = 0)
    {
        TopBalances = topBalances;
        Total = total;
        HiddenDownlinesCount = hiddenDownlinesCount;
    }

    public IEnumerable<TopBalance> TopBalances { get; }

    public TopBalance Total { get; }

    public int HiddenDownlinesCount { get; }
}
