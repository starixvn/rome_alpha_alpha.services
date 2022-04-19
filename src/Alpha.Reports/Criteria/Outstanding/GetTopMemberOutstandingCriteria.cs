namespace Alpha.Reports.Criteria.Outstanding;

public class GetTopMemberOutstandingCriteria : GetTopOutstandingCriteria
{
    public GetTopMemberOutstandingCriteria(int custId, string productIds, int pageSize)
        : base(custId, productIds, pageSize)
    {
    }

    public decimal TotalMemberOutstanding { get; set; }

    public decimal TotalMasterOutstanding { get; set; }

    public decimal TotalAgentOutstanding { get; set; }

    public override string GetSettingKey()
        => "Age_Rpt_Outstanding_Member";

    public override bool IsValid() => CustId > 0;
}
