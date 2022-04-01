namespace Alpha.Reports.Models.Outstanding;

public record TopOutstanding
{
    public TopOutstanding() : this(
        Enumerable.Empty<DownlineOutstanding>(),
        new DownlineOutstanding(0, "Total", 0, 0))
    {
    }

    public TopOutstanding(
        IEnumerable<DownlineOutstanding> topOutstandings,
        DownlineOutstanding total,
        int hiddenDownlinesCount = 0)
    {
        TopOutstandings = topOutstandings;
        Total = total;
        HiddenDownlinesCount = hiddenDownlinesCount;
    }

    public IEnumerable<DownlineOutstanding> TopOutstandings { get; }

    public DownlineOutstanding Total { get; }

    public int HiddenDownlinesCount { get; }
}
