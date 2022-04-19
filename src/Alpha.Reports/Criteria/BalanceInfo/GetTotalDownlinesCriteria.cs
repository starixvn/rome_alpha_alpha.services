using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetTotalDownlinesCriteria : CriteriaBase
{
    public GetTotalDownlinesCriteria(int custId, byte roleId)
    {
        CustId = custId;
        RoleId = roleId;
    }

    public int CustId { get; }

    public byte RoleId { get; }

    public override string GetSettingKey() => "Acc_CustomerSelTotalDownline";

    public override bool IsValid() => CustId > 0;
}