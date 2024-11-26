namespace Alpha.Reports.Models.BalanceInfo;

public record AgentBalance
{
    public AgentBalance()
    {
    }

    public AgentBalance(
        decimal agentCash,
        decimal yesterdayAgentCash,
        decimal agentPositionTaking,
        decimal customerCash,
        decimal yesterdayCustomerCash,
        decimal customerOutstanding,
        decimal pendingTransfer3rd,
        decimal? rewardReceivable,
        decimal? rewardPayable,
        string rewardCurrency)
    {
        AgentCash = agentCash;
        YesterdayAgentCash = yesterdayAgentCash;
        AgentPositionTaking = agentPositionTaking;
        CustomerCash = customerCash;
        YesterdayCustomerCash = yesterdayCustomerCash;
        CustomerOutstanding = customerOutstanding;
        PendingTransfer3rd = pendingTransfer3rd;
        RewardReceivable = rewardReceivable;
        RewardPayable = rewardPayable;
        RewardCurrency = rewardCurrency;
    }

    public decimal AgentCash { get; }

    public decimal YesterdayAgentCash { get; }

    public decimal AgentPositionTaking { get; }

    public decimal CustomerCash { get; }

    public decimal YesterdayCustomerCash { get; }

    public decimal CustomerOutstanding { get; }

    public decimal PendingTransfer3rd { get; }

    public decimal? RewardReceivable { get; }

    public decimal? RewardPayable { get; }

    public string RewardCurrency { get; }
}
