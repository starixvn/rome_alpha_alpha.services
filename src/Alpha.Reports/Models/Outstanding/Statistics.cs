namespace Alpha.Reports.Models.Outstanding;

public record Statistics
{
    public Statistics()
    {
    }

    public Statistics(decimal outStanding,decimal totalOutStanding)
    {
        Outstanding = outStanding;
        TotalOutstanding = totalOutStanding;
    }

    public decimal Outstanding { get; }

    public decimal TotalOutstanding { get; }
}