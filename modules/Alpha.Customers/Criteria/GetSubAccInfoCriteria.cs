using Fanex.Data.Repository;

namespace Alpha.Customers.Criteria;

public class GetSubAccInfoCriteria : CriteriaBase
{
    public GetSubAccInfoCriteria(int custId, int subAccId)
    {
        CustId = custId;
        SubAccId = subAccId;
    }

    public int CustId { get; }

    public int SubAccId { get; }

    public override string GetSettingKey() => "Age_CustSubSelInfo";

    public override bool IsValid() => CustId > 0 && SubAccId > 0;
}
