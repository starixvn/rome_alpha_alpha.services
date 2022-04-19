using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetDownlineBalanceCriteria : CriteriaBase
{
    public GetDownlineBalanceCriteria(int custId, UserLevel custLevel)
    {
        CustId = custId;
        CustLevel = custLevel;
    }
    
    public int CustId { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsMaster()
        ? "Age_BalanceMasterSel"
        : CustLevel.IsAgent()
            ? "Age_BalanceAgentSel"
            : "Age_BalanceMemberSel";

    public override bool IsValid() => CustId > 0;
}
