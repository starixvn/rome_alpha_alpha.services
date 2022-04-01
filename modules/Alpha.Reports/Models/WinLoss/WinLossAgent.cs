namespace Alpha.Reports.Models.WinLoss;

public record WinLossAgent(
    decimal Turnover,
    decimal GrossComm,
    decimal MemberWinLoss,
    decimal MemberComm,
    decimal AgentWinLoss,
    decimal AgentComm) : WinLossBase(Turnover,GrossComm )
{
    public decimal MemberTotal => MemberWinLoss + MemberComm;

    public decimal AgentTotal => AgentWinLoss + AgentComm;

    public decimal CompanyWinLoss => -(MemberTotal + AgentTotal);
}
