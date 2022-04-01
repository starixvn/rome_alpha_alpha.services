using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetTopBalanceCriteria : CriteriaBase
{
    public GetTopBalanceCriteria(
        int custId,
        UserLevel custLevel,
        string username = "",
        bool? enabled = null,
        bool? closed = false,
        bool? suspended = null,
        int pageIndex = 1,
        int pageSize = int.MaxValue - 1)
    {
        CustId = custId;
        Username = username;
        Enabled = enabled;
        Closed = closed;
        Suspended = suspended;
        PageIndex = pageIndex;
        PageSize = pageSize;
        CustLevel = custLevel;
    }

    public int CustId { get; }

    public string Username { get; } = default!;

    public bool? Enabled { get; }

    public bool? Closed { get; }

    public bool? Suspended { get; }

    public int PageIndex { get; }

    public int PageSize { get; }

    public int TotalItems { get; set; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
        => CustLevel.IsSuper()
        ? "Age_CustomerSelCreditBalanceList_Sag"
        : CustLevel.IsMaster()
            ? "Age_CustomerSelCreditBalanceList_Mag"
            : "Age_CustomerSelCreditBalanceList_Ag";
    
    public override bool IsValid() => CustId > 0;
}
