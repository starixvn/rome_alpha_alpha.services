namespace Alpha.Customers.Criteria.AutoTransfer;

public class GetMastersCriteria : GetCustomersCriteria
{
    public int SAgId { get; set; }

    public override string GetSettingKey() => $"SiteDB{SiteDBGroupID}_Age_Transfer_SelList_Level4";

    public override bool IsValid() => SAgId > 0;
}