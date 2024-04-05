using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetTotalBetsDetailsCriteria : CriteriaBase
{
    public GetTotalBetsDetailsCriteria(int custId, long matchId, bool isApplyPT) {
        CustId = custId;
        MatchId = matchId;
        IsApplyPT = isApplyPT;
    }

    public int CustId { get; }

    public long MatchId { get; }

    public bool IsApplyPT { get; }

    public override string GetSettingKey() => "Age_TotalBets_ChatBot_SelDetail";

    public override bool IsValid() => CustId > 0 && MatchId > 0;
}