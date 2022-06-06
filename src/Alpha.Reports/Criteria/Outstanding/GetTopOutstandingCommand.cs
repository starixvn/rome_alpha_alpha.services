using Alpha.Shared.Enumerations;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.Outstanding;

public class GetTopOutstandingCommand : NonQueryCommand
{
    public GetTopOutstandingCommand(int custId, UserLevel custLevel, string productIds, int pageSize)
    {
        CustId = custId;
        CustLevel = custLevel;
        ProductIds = productIds;
        PageSize = pageSize;
    }

    public int CustId { get; }

    public string ProductIds { get; }

    public bool IsChatBot { get; } = true;

    public int PageSize { get; }

    public decimal TotalMemberOutstanding { get; set; }

    public decimal? TotalAgentOutstanding { get; set; }

    public decimal? TotalMasterOutstanding { get; set; }

    public decimal? TotalSuperOutstanding { get; set; }

    [SpParam(Ignored = true)]
    public UserLevel CustLevel { get; }

    public override string GetSettingKey()
    {
        if (CustLevel.IsSuper())
        {
            return "Age_Rpt_Outstanding_Master";
        }

        if (CustLevel.IsMaster())
        {
            return "Age_Rpt_Outstanding_Agent";
        }

        return "Age_Rpt_Outstanding_Member";
    }

    public override bool IsValid() => CustId > 0;
}
