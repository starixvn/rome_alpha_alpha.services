using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetMemberBalanceCommand : NonQueryCommand
{
    public GetMemberBalanceCommand(int custId)
        => CustId = custId;

    public int CustId { get; }

    public decimal Balance { get; set; }

     public decimal NetPosition { get; set; }
        
    public decimal PendingTransfer3rd  { get; set; }

    public override string GetSettingKey() => "Acc_BalanceMemberSel";

    public override bool IsValid() => CustId > 0;
}