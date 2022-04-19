using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.Outstanding;

public class GetTopOutstandingCriteria : NonQueryCommand
{
    protected GetTopOutstandingCriteria(int custId, string productIds, int pageSize)
    {
        CustId = custId;
        ProductIds = productIds;
        PageSize = pageSize;
    }

    public int CustId { get; }

    public string ProductIds { get; }

    public bool IsChatBot { get; } = true;

    public int PageSize { get; }

    public override string GetSettingKey()
        => throw new NotImplementedException();

    public override bool IsValid() => CustId > 0;
}
