namespace Alpha.Customers.Criteria.AutoTransfer;

public class GetMembersCriteria : GetCustomersCriteria
{
    public int CustId { get; set; }

    public override string GetSettingKey() => $"SiteDB{SiteDBGroupID}_Age_Transfer_SelList_Level2";

    public override bool IsValid() => CustId > 0;
}