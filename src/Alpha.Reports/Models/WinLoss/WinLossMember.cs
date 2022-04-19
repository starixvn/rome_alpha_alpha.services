namespace Alpha.Reports.Models.WinLoss;

public record WinLossMember : WinLossBase
{
    public decimal MemberWinLoss { get; init; }

    public decimal MemberComm { get; init; }

    public decimal AgentWinLoss { get; init; }

    public decimal AgentComm { get; init; }

    public decimal MemberTotal => MemberWinLoss + MemberComm;

    public decimal AgentTotal => AgentWinLoss + AgentComm;

    public decimal CompanyWinLoss => -(MemberTotal + AgentTotal);

    public static WinLossMember operator+ (WinLossMember m1, WinLossMember m2)
        => new WinLossMember
        {
            Turnover = m1.Turnover + m2.Turnover,
            GrossComm = m1.GrossComm + m2.GrossComm,
            MemberWinLoss = m1.MemberWinLoss + m2.MemberWinLoss,
            MemberComm = m1.MemberComm + m2.MemberComm,
            AgentWinLoss = m1.AgentWinLoss + m2.AgentWinLoss,
            AgentComm = m1.AgentComm + m2.AgentComm
        };
}
