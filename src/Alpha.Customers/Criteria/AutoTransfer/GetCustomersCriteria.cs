using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Customers.Criteria.AutoTransfer;

public class GetCustomersCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public string SiteDBGroupID { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public string Username { get; set; }

    public string CustStatus { get; set; }

    public string AutoTransfer { get; set; }

    public bool IsYesterdayBalance => false;

    public bool IsAllowTotalBalance => true;

    public int TotalPages { get; set; }

    public override string GetSettingKey() => "";

    public override bool IsValid() => true;
}
