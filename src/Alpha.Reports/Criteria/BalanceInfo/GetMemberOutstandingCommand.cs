using Fanex.Data.Repository;

namespace Alpha.Reports.Criteria.BalanceInfo;

public class GetMemberOutstandingCommand : NonQueryCommand
{
    public GetMemberOutstandingCommand(int custId)
        => CustId = custId;

    public int CustId { get; }

    public decimal Outstanding { get; set; }

    public override string GetSettingKey() => "Acc_OutstandingMemberSel";

    public override bool IsValid() => CustId > 0;
}