namespace Alpha.Reports.Models.BalanceInfo;

public class CustomerBalance
{
    public CustomerBalance()
    {
    }

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
        decimal pendingTransfer3rd)
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

    public decimal Outstanding { get; set; }

    public decimal TotalOutstanding { get; set; }
}