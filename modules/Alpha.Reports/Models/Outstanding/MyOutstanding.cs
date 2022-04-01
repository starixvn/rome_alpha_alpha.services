namespace Alpha.Reports.Models.Outstanding;

public record MyOutstanding
{
    public MyOutstanding()
    {
    }

    public MyOutstanding(decimal outstanding, decimal totalOutstanding)
    {
        Outstanding = outstanding;
        TotalOutstanding = totalOutstanding;
    }

    public decimal Outstanding { get; }

    public decimal TotalOutstanding { get; }
}

