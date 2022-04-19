namespace Alpha.API.Client.Models.BalanceInfo;

public record CustomerBalance(
    string CurrencyName,
    decimal CashBalance,
    decimal YesterdayCashBalance,
    decimal TotalBalance,
    decimal YesterdayTotalBalance,
    decimal TodayWinLoss,
    decimal YesterdayWinLoss,
    decimal GivenCredit,
    decimal MemberCredit,
    decimal PendingTransfer3rd,
    decimal Outstanding,
    decimal TotalOutstanding);
