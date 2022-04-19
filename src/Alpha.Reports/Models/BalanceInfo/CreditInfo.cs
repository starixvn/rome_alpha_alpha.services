namespace Alpha.Reports.Models.BalanceInfo;

public record CreditInfo
{
    public CreditInfo()
    {
    }

    public CreditInfo(decimal credit, decimal usedCredit, decimal? memberMaxCredit)
    {
        GivenCredit = credit;
        UsedCredit = usedCredit;
        MemberMaxCredit = memberMaxCredit;
    }

    public decimal GivenCredit { get; }

    public decimal UsedCredit { get; }

    public decimal? MemberMaxCredit { get; }
}