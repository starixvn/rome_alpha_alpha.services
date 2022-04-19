using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetCreditInfoCriteria : CriteriaBase
{
    public GetCreditInfoCriteria(int custId)
        => CustId = custId;

    public int CustId { get; }

    public override string GetSettingKey() => "Age_Customer_CreditInfo_Sel";

    public override bool IsValid() => CustId > 0;
}