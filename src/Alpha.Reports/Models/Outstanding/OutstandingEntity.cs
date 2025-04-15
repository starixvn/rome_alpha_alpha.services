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
        decimal? superOutstanding,
        decimal? toWin,
        decimal? agentRisk,
        decimal? masterRisk,
        decimal? superRisk)
    {
        Id = id;
        Name = name;
        MemberOutstanding = memberOutstanding;
        AgentOutstanding = agentOutstanding;
        MasterOutstanding = masterOutstanding;
        SuperOutstanding = superOutstanding;

        ToWin = toWin;
        AgentRisk = agentRisk;
        MasterRisk = masterRisk;
        SuperRisk = superRisk;
    }

    // CustId or ProductId
    public int Id { get; }

    // CustName or ProductName
    public string Name { get; }

    public decimal MemberOutstanding { get; }

    public decimal? AgentOutstanding { get; }

    public decimal? MasterOutstanding { get; }

    public decimal? SuperOutstanding { get; }

    public bool IsDirectMember { get; }
    
    public decimal? ToWin { get; }

    public decimal? AgentRisk { get; }

    public decimal? MasterRisk { get; }

    public decimal? SuperRisk { get; }
}
