using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetTopBalanceCriteria : CriteriaBase
{
    public GetTopBalanceCriteria(
        int custId,
        UserLevel custLevel,
        int pageSize)
    {
        CustId = custId;
        CustLevel = custLevel;
        PageSize = pageSize;
    }

    public int CustId { get; }

    public int PageSize { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? "Age_CustomerSelBalanceList_Master"
        : CustLevel.IsMaster()
            ? "Age_CustomerSelBalanceList_Agent"
            : "Age_CustomerSelBalanceList_Member";
    
    public override bool IsValid() => CustId > 0;
}
