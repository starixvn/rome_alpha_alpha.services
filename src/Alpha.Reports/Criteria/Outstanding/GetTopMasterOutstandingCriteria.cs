namespace Alpha.Reports.Criteria.Outstanding;

public class GetTopMasterOutstandingCriteria : GetTopOutstandingCriteria
{
    public GetTopMasterOutstandingCriteria(int custId, string productIds, int pageSize)
        : base(custId, productIds, pageSize)
    {
    }

    public decimal TotalMemberOutstanding { get; set; }

    public decimal TotalSuperOutstanding { get; set; }

    public override string GetSettingKey()
        => "Age_Rpt_Outstanding_Master";

    public override bool IsValid() => CustId > 0;
}
