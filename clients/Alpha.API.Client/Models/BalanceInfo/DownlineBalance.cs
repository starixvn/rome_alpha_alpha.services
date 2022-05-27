using Alpha.API.Client.Models.Outstanding;

namespace Alpha.API.Client.Models.BalanceInfo;

public record DownlineBalance(
    decimal? TotalCash,
    decimal? YesterdayCash,
    decimal TotalBalance,
    decimal YesterdayBalance,
    decimal PendingTransfer3rd,
    decimal? UsedCredit,
    decimal GivenCredit,
    decimal? MemberMaxCredit,
    IEnumerable<TotalDownline> TotalDownlines,
    decimal? NetPosition,
    decimal? BetCredit,
    CustomerOutstanding Outstanding
);
