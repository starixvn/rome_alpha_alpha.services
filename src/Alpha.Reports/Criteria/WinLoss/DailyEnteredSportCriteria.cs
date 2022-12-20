using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.WinLoss;

public class DailyEnteredSportCriteria : CriteriaBase
{
    public DailyEnteredSportCriteria(bool includeTestData)
    {
        IncludeTestData = includeTestData;
    }

    public bool IncludeTestData { get; }

    public override string GetSettingKey() => "Age_Rpt_MatchSelResultEntered";

    public override bool IsValid() => true;
}