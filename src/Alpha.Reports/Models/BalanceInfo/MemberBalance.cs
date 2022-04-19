namespace Alpha.Reports.Models.BalanceInfo;

public class MemberBalance
{
    public MemberBalance()
    {
    }

    public MemberBalance(decimal yesterdayCash)
        => YesterdayCustomerCash = yesterdayCash;

    public decimal Balance { get; set; }

    public decimal NetPosition  { get; set; }

    public decimal CustomerCash => Balance + CustomerOutstanding + PendingTransfer3rd;

    public decimal YesterdayCustomerCash { get; }

    public decimal CustomerOutstanding  { get; set; }

    public decimal PendingTransfer3rd { get; set; }
}
