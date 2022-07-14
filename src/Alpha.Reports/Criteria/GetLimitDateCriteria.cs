using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria;

public class GetLimitDateCriteria : CriteriaBase
{
    public GetLimitDateCriteria(int custId)
        => CustId = custId;

    public int CustId { get; }

    public override string GetSettingKey() => "Age_CustomerSelDateLimit";

    public override bool IsValid() => CustId > 0;
}
