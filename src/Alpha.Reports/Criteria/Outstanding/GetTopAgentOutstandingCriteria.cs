namespace Alpha.Reports.Criteria.Outstanding;

public class GetTopAgentOutstandingCriteria : GetTopOutstandingCriteria
{
    public GetTopAgentOutstandingCriteria(int custId, string productIds, int pageSize)
        : base(custId, productIds, pageSize)
    {
    }

    public decimal TotalMemberOutstanding { get; set; }

    public decimal TotalMasterOutstanding { get; set; }

    public decimal TotalSuperOutstanding { get; set; }

    public override string GetSettingKey()
        => "Age_Rpt_Outstanding_Agent";

    public override bool IsValid() => CustId > 0;
}
