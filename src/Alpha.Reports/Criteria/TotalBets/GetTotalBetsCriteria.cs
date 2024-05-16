using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetTotalBetsCriteria : CriteriaBase
{
    public GetTotalBetsCriteria(int custId, bool isApplyPT) {
        CustId = custId;
        IsApplyPT = isApplyPT;
    }

    public int CustId { get; }

    public bool IsApplyPT { get; }

    public override string GetSettingKey() => "Age_TotalBets_ChatBot_SelEvent";

    public override bool IsValid() => CustId > 0;
}