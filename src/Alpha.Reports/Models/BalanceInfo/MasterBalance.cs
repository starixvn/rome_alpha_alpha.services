namespace Alpha.Reports.Models.BalanceInfo;

public record MasterBalance
{
    public MasterBalance()
    {
    }

#pragma warning disable S107
    public MasterBalance(
        decimal masterCash,
        decimal yesterdayMasterCash,
        decimal masterPositionTaking,
        decimal agentCash,
        decimal yesterdayAgentCash,
        decimal customerCash,
        decimal yesterdayCustomerCash,
        decimal customerOutstanding,
        decimal pendingTransfer3rd,
        decimal? rewardReceivable,
        decimal? rewardPayable,
        string rewardCurrency)
#pragma warning restore S107
    {
        MasterCash = masterCash;
        YesterdayMasterCash = yesterdayMasterCash;
        MasterPositionTaking = masterPositionTaking;
        AgentCash = agentCash;
        YesterdayAgentCash = yesterdayAgentCash;
        CustomerCash = customerCash;
        YesterdayCustomerCash = yesterdayCustomerCash;
        CustomerOutstanding = customerOutstanding;
        PendingTransfer3rd = pendingTransfer3rd;
        RewardReceivable = rewardReceivable;
        RewardPayable = rewardPayable;
        RewardCurrency = rewardCurrency;
    }

    public decimal MasterCash { get; }

    public decimal YesterdayMasterCash { get; }

    public decimal MasterPositionTaking { get; }

    public decimal AgentCash { get; }

    public decimal YesterdayAgentCash { get; }

    public decimal CustomerCash { get; }

    public decimal YesterdayCustomerCash { get; }

    public decimal CustomerOutstanding { get; }

    public decimal PendingTransfer3rd { get; }

    public decimal? RewardReceivable { get; }

    public decimal? RewardPayable { get; }

    public string RewardCurrency { get; }
}