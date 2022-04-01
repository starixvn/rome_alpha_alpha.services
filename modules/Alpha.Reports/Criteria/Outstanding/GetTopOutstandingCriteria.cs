using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.Outstanding;

public class GetTopOutstandingCriteria : CriteriaBase
{
    public GetTopOutstandingCriteria(int custId, UserLevel custLevel, string productIds)
    {
        CustId = custId;
        CustLevel = custLevel;
        ProductIds = productIds;
    }

    public int CustId { get; }

    public string ProductIds { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? "Age_Rpt_Outstanding_Master"
        : CustLevel.IsMaster()
            ? "Age_Rpt_Outstanding_Agent"
            : "Age_Rpt_Outstanding_Member";

    public override bool IsValid() => CustId > 0;
}
