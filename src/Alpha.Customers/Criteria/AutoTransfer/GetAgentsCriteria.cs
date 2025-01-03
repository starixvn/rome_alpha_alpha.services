namespace Alpha.Customers.Criteria.AutoTransfer;

public class GetAgentsCriteria : GetCustomersCriteria
{
    public int MasterId { get; set; }

    public override string GetSettingKey() => $"SiteDB{SiteDBGroupID}_Age_Transfer_SelList_Level3";

    public override bool IsValid() => MasterId > 0;
}