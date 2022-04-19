namespace Alpha.API.Client.Models.BalanceInfo;

public record DownlineBalance(
    decimal? TotalCash,
    decimal? YesterdayCash,
    decimal TotalBalance,
    decimal YesterdayBalance,
    decimal Outstanding,
    decimal PendingTransfer3rd,
    decimal? UsedCredit,
    decimal GivenCredit,
    decimal? MemberMaxCredit,
    IEnumerable<TotalDownline> TotalDownlines,
    decimal? NetPosition,
    decimal? BetCredit,
    decimal UplineOutstanding,
    decimal TotalOutstanding
);
