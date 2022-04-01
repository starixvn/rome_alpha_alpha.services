using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria;

public class GetOfferedProductsCriteria : CriteriaBase
{
    public GetOfferedProductsCriteria(int custId, byte settingStatus)
    {
        CustId = custId;
        SettingStatus = settingStatus;
    }

    public int CustId { get; }

    public byte SettingStatus { get; }

    public override string GetSettingKey() => "Age_GetOfferedProductByCust";

    public override bool IsValid() => CustId > 0;
}
