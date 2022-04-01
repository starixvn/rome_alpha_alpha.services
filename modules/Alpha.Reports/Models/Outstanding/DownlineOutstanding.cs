namespace Alpha.Reports.Models.Outstanding;

public class DownlineOutstanding
{
    public DownlineOutstanding()
    {
    }

    public DownlineOutstanding(int id, string name, decimal memberOutstanding, decimal outstanding)
    {
        Id = id;
        Name = name;
        MemberOutstanding = memberOutstanding;
        Outstanding = outstanding;
    }

    // CustId or ProductId
    public int Id { get; }

    // CustName or ProductName
    public string Name { get; set; } = default!;

    public decimal MemberOutstanding { get; }

    public decimal Outstanding { get; }
}
