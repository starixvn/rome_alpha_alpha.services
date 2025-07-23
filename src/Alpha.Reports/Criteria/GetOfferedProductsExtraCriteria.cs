using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria;

public class GetOfferedProductsExtraCriteria : CriteriaBase
{
    public GetOfferedProductsExtraCriteria(int custId, byte settingStatus, string cashServiceSites)
    {
        CustId = custId;
        SettingStatus = settingStatus;
        CashServiceSites = cashServiceSites;
    }

    public int CustId { get; }

    public byte SettingStatus { get; }

    public string CashServiceSites { get; }

    public override string GetSettingKey() => "Age_GetOfferedProductByCust_Extra";

    public override bool IsValid() => CustId > 0;
}
