using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetMemberBalanceCommand : NonQueryCommand
{
    public GetMemberBalanceCommand(int custId, int siteDBGroupID)
        => (CustId, SiteDBGroupID) = (custId, siteDBGroupID);

    [SpParam(Ignored = true)]
    public int SiteDBGroupID { get; set; }

    public int CustId { get; }

    public decimal Balance { get; set; }

    public decimal NetPosition { get; set; }

    public decimal PendingTransfer3rd { get; set; }

    public override string GetSettingKey() => $"SiteDB{SiteDBGroupID}_Acc_BalanceMemberSel";

    public override bool IsValid() => CustId > 0;
}