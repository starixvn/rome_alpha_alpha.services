using Alpha.Reports.Models.Outstanding;

namespace Alpha.Reports.Models.BalanceInfo;

public record DownlineBalance
{
    public DownlineBalance(
        MasterBalance balance,
        CreditInfo creditInfo,
        Statistics statistics,
        IEnumerable<TotalDownline> totalDownlines)
    {
        TotalCash = RoundUpNumber(balance.MasterCash);
        YesterdayCash = RoundUpNumber(balance.YesterdayMasterCash);
        TotalBalance = RoundUpNumber(balance.MasterCash + balance.AgentCash + balance.CustomerCash);
        YesterdayBalance = RoundUpNumber(balance.YesterdayMasterCash + balance.YesterdayAgentCash + balance.YesterdayCustomerCash);
        Outstanding = RoundUpNumber(balance.MasterPositionTaking);
        PendingTransfer3rd = balance.PendingTransfer3rd;
        UsedCredit = RoundUpNumber(creditInfo.UsedCredit);
        GivenCredit = RoundUpNumber(creditInfo.GivenCredit);
        UplineOutstanding = RoundUpNumber(statistics.Outstanding);
        TotalOutstanding = RoundUpNumber(statistics.TotalOutstanding);
        TotalDownlines = totalDownlines;
    }

    public DownlineBalance(
        AgentBalance balance,
        CreditInfo creditInfo,
        Statistics statistics,
        IEnumerable<TotalDownline> totalDownlines)
    {
        TotalCash = RoundUpNumber(balance.AgentCash);
        YesterdayCash = RoundUpNumber(balance.YesterdayAgentCash);
        TotalBalance = RoundUpNumber(balance.AgentCash + balance.CustomerCash);
        YesterdayBalance = RoundUpNumber(balance.YesterdayAgentCash + balance.YesterdayCustomerCash);
        Outstanding = RoundUpNumber(balance.AgentPositionTaking);
        PendingTransfer3rd = balance.PendingTransfer3rd;
        UsedCredit = RoundUpNumber(creditInfo.UsedCredit);
        GivenCredit = RoundUpNumber(creditInfo.GivenCredit);
        MemberMaxCredit = RoundUpNumber(creditInfo.MemberMaxCredit ?? 0);
        UplineOutstanding = RoundUpNumber(statistics.Outstanding);
        TotalOutstanding = RoundUpNumber(statistics.TotalOutstanding);
        TotalDownlines = totalDownlines;
    }

    public DownlineBalance(MemberBalance balance, CreditInfo creditInfo, Statistics statistics)
    {
        TotalBalance = RoundUpNumber(balance.CustomerCash);
        YesterdayBalance = RoundUpNumber(balance.YesterdayCustomerCash);
        Outstanding = RoundUpNumber(balance.CustomerOutstanding);
        PendingTransfer3rd = balance.PendingTransfer3rd;
        GivenCredit = RoundUpNumber(creditInfo.GivenCredit);
        NetPosition = RoundDownNumber(balance.NetPosition);
        BetCredit = RoundDownNumber(creditInfo.GivenCredit + balance.Balance + balance.NetPosition);
        UplineOutstanding = RoundUpNumber(statistics.Outstanding);
        TotalOutstanding = RoundUpNumber(statistics.TotalOutstanding);
    }

    public decimal? TotalCash { get; }

    public decimal? YesterdayCash { get; }

    public decimal TotalBalance { get; }

    public decimal YesterdayBalance { get; }

    public decimal Outstanding { get; }

    public decimal PendingTransfer3rd { get; }

    public decimal? UsedCredit { get; }

    public decimal GivenCredit { get; }

    public decimal? MemberMaxCredit { get; }

    public IEnumerable<TotalDownline> TotalDownlines { get; } = default!;

    public decimal? NetPosition { get; }

    public decimal? BetCredit { get; }

    public decimal UplineOutstanding { get; }

    public decimal TotalOutstanding { get; }

    private static decimal RoundUpNumber(decimal value)
        => Math.Round(value, 2, MidpointRounding.AwayFromZero);

    private static decimal RoundDownNumber(decimal value)
        => Math.Truncate(100 * value) / 100;
}