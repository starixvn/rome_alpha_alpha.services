using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.Outstanding;

public class GetCustomerOutstandingCriteria : CriteriaBase
{
    public GetCustomerOutstandingCriteria(int custId)
        => CustId = custId;
    
    public int CustId { get; }

    public override string GetSettingKey()
        => "Age_CustomerSelOutstanding";

    public override bool IsValid() => CustId > 0;
}
