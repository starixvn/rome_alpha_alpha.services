using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetDownlineBalanceCriteria : CriteriaBase
{
    public GetDownlineBalanceCriteria(UserLevel custLevel, int downlineId)
    {
        CustLevel = custLevel;
        DownlineId = downlineId;
    }
    
    public int DownlineId { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? ""
        : CustLevel.IsMaster()
            ? ""
            : "";

    public override bool IsValid() => DownlineId > 0;
}
