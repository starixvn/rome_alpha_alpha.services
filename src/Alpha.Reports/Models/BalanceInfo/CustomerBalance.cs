using Alpha.Reports.Models.Outstanding;

namespace Alpha.Reports.Models.BalanceInfo;

public class CustomerBalance
{
    public CustomerBalance()
    {
    }

#pragma warning disable S107
    public CustomerBalance(
        string currencyName,
        decimal cashBalance,
        decimal yesterdayCashBalance,
        decimal totalBalance,
        decimal yesterdayTotalBalance,
        decimal todayWinLoss,
        decimal yesterdayWinLoss,
        decimal givenCredit,
        decimal memberCredit,
        decimal pendingTransfer3rd,
        CustomerOutstanding outstanding,
        decimal? rewardReceivable,
        decimal? rewardPayable,
        string rewardCurrency,
        decimal? directMemberCredit)
#pragma warning restore S107
    {
        CurrencyName = currencyName;
        CashBalance = cashBalance;
        YesterdayCashBalance = yesterdayCashBalance;
        TotalBalance = totalBalance;
        YesterdayTotalBalance = yesterdayTotalBalance;
        TodayWinLoss = todayWinLoss;
        YesterdayWinLoss = yesterdayWinLoss;
        GivenCredit = givenCredit;
        MemberCredit = memberCredit;
        PendingTransfer3rd = pendingTransfer3rd;
        Outstanding = outstanding;
        RewardReceivable = rewardReceivable;
        RewardPayable = rewardPayable;
        RewardCurrency = rewardCurrency;
        DirectMemberCredit = directMemberCredit;
    }

    public string CurrencyName { get; } = default!;

    public decimal CashBalance { get; }

    public decimal YesterdayCashBalance { get; }

    public decimal TotalBalance { get; }

    public decimal YesterdayTotalBalance { get; }

    public decimal TodayWinLoss { get; }

    public decimal YesterdayWinLoss { get; }

    public decimal GivenCredit { get; }

    public decimal MemberCredit { get; }

    public decimal PendingTransfer3rd { get; }

    public CustomerOutstanding Outstanding { get; set; }

    public decimal? RewardReceivable { get; }

    public decimal? RewardPayable { get; }

    public string RewardCurrency { get; }

    public decimal? DirectMemberCredit { get; }
}