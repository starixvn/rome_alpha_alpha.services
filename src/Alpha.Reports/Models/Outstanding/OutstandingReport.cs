namespace Alpha.Reports.Models.Outstanding;

public record OutstandingReport
{
    public OutstandingReport() : this(Enumerable.Empty<OutstandingModel>())
    {
    }

    public OutstandingReport(IEnumerable<OutstandingModel> outstandings, OutstandingModel total = null)
    {
        Outstandings = outstandings;
        Total = total;
    }

    public IEnumerable<OutstandingModel> Outstandings { get; }

    public OutstandingModel Total { get; }
}
