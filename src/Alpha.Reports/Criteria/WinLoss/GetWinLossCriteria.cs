using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.WinLoss;

public class GetWinLossCriteria : CriteriaBase
{
    public GetWinLossCriteria(
        int custId,
        DateTime fromDate,
        DateTime toDate,
        string productIds,
        DateTime moveBetsDay,
        DateTime balanceUpDay,
        UserLevel custLevel,
        int? downlineId)
    {
        CustId = custId;
        FromDate = fromDate;
        ToDate = toDate;
        ProductIds = productIds;
        MoveBetsDay = moveBetsDay;
        BalanceUpDay = balanceUpDay;
        CustLevel = custLevel;
        DownlineId = downlineId;
    }

    public int CustId { get; }

    public DateTime FromDate { get; }

    public DateTime ToDate { get; }

    public string ProductIds { get; }

    public DateTime MoveBetsDay { get; }

    public DateTime BalanceUpDay { get; }

    public int? DownlineId { get; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
    {
        if (CustLevel.IsSuper())
        {
            return "Age_Rpt_WinLoss_Master";
        }

        if (CustLevel.IsMaster())
        {
            return "Age_Rpt_WinLoss_Agent";
        }

        return "Age_Rpt_WinLoss_Member";
    }

    public override bool IsValid() => CustId > 0 && FromDate <= ToDate;
}
