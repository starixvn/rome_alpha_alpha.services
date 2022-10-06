using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria;

public class GetUMInfoCriteria : CriteriaBase
{
    public GetUMInfoCriteria(int custId)
        => CustId = custId;

    public int CustId { get; }

    public override string GetSettingKey() => "Acc_CheckUM_ByReport";

    public override bool IsValid() => CustId > 0;
}
