using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.Outstanding;

public class GetDownlineOutstandingCriteria : CriteriaBase
{
    public GetDownlineOutstandingCriteria(UserLevel custLevel, string productIds, int downlineId)
    {
        CustLevel = custLevel;
        ProductIds = productIds;
        DownlineId = downlineId;
    }
    
    public string ProductIds { get; }

    public int DownlineId { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? "Age_Rpt_DownlineOutstanding_Master"
        : CustLevel.IsMaster()
            ? "Age_Rpt_DownlineOutstanding_Agent"
            : "Age_Rpt_DownlineOutstanding_Member";

    public override bool IsValid() => DownlineId > 0;
}
