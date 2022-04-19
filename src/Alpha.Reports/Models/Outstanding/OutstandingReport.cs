namespace Alpha.Reports.Models.Outstanding;

public record OutstandingReport
{
    public OutstandingReport() : this(Enumerable.Empty<OutstandingEntity>())
    {
    }

    public OutstandingReport(IEnumerable<OutstandingEntity> outstandings, OutstandingEntity total = null)
    {
        Outstandings = outstandings;
        Total = total;
    }

    public IEnumerable<OutstandingEntity> Outstandings { get; }

    public OutstandingEntity Total { get; }
}
