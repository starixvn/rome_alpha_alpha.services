namespace Alpha.Reports.Models.Outstanding;

public record OutstandingEntity
{
    public OutstandingEntity()
    {
    }

    public OutstandingEntity(
        int id,
        string name,
        decimal memberOutstanding,
        decimal? agentOutstanding,
        decimal? masterOutstanding,
        decimal? superOutstanding)
    {
        Id = id;
        Name = name;
        MemberOutstanding = memberOutstanding;
        AgentOutstanding = agentOutstanding;
        MasterOutstanding = masterOutstanding;
        SuperOutstanding = superOutstanding;
    }

    // CustId or ProductId
    public int Id { get; }

    // CustName or ProductName
    public string Name { get; }

    public decimal MemberOutstanding { get; }

    public decimal? AgentOutstanding { get; }

    public decimal? MasterOutstanding { get; }

    public decimal? SuperOutstanding { get; }
}
