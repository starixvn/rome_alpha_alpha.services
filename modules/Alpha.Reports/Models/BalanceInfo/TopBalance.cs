namespace Alpha.Reports.Models.BalanceInfo;

public record TopBalance
{
    public TopBalance()
    {
    }

    public TopBalance(string username, decimal balance, decimal yesterdayBalance)
    {
        Username = username;
        Balance = balance;
        YesterdayBalance = yesterdayBalance;
    }

    public string Username { get; } = default!;

    public decimal Balance { get; }

    public decimal YesterdayBalance { get; }
}
