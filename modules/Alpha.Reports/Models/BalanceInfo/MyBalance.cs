namespace Alpha.Reports.Models.BalanceInfo;

public record MyBalance
{
    public MyBalance()
    {
    }

    public MyBalance(
        decimal cashBalance,
        decimal yesterdayCashBalance,
        decimal totalBalance,
        decimal yesterdayTotalBalance,
        decimal todayWinLoss,
        decimal yesterdayWinLoss,
        decimal givenCredit,
        decimal memberCredit)
    {
        CashBalance = cashBalance;
        YesterdayCashBalance = yesterdayCashBalance;
        TotalBalance = totalBalance;
        YesterdayTotalBalance = yesterdayTotalBalance;
        TodayWinLoss = todayWinLoss;
        YesterdayWinLoss = yesterdayWinLoss;
        GivenCredit = givenCredit;
        MemberCredit = memberCredit;
    }

    public decimal CashBalance { get; }

    public decimal YesterdayCashBalance { get; }

    public decimal TotalBalance { get; }

    public decimal YesterdayTotalBalance { get; }

    public decimal TodayWinLoss { get; }

    public decimal YesterdayWinLoss { get; }

    public decimal GivenCredit { get; }

    public decimal MemberCredit { get; }
}