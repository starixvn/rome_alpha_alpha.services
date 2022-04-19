namespace Alpha.Customers.Params;

public record CustomerStatusParam(int CustId, int SubAccId)
{
    public string ToXml()
        => $"<r id=\"{CustId}\" sub=\"{SubAccId}\"/>";
}