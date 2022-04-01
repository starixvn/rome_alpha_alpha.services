using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.Outstanding;

public class GetMyOutstandingCriteria : CriteriaBase
{
    public GetMyOutstandingCriteria(int custId, UserLevel custLevel)
    {
        CustId = custId;
        CustLevel = custLevel;
    }

    public int CustId { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? "Age_CustomerSelStatisticsInfo_SAg"
        : CustLevel.IsMaster()
            ? "Age_CustomerSelStatisticsInfo_MAg"
            : "Age_CustomerSelStatisticsInfo_Ag";

    public override bool IsValid() => CustId > 0;
}
