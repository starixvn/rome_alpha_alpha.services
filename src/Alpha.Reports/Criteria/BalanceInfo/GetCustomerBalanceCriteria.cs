using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetCustomerBalanceCriteria : CriteriaBase
{
    public GetCustomerBalanceCriteria(int custId, UserLevel custLevel, DateTime moveBetsDay)
    {
        CustId = custId;
        CustLevel = custLevel;
        MoveBetsDay = moveBetsDay;
    }

    public int CustId { get; }

    public DateTime MoveBetsDay { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? "Age_CustomerSelBalanceInfo_SAg"
        : CustLevel.IsMaster()
            ? "Age_CustomerSelBalanceInfo_MAg"
            : "Age_CustomerSelBalanceInfo_Ag";

    public override bool IsValid() => CustId > 0;
}
