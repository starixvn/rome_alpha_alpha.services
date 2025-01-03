namespace Alpha.Reports.Models.BalanceInfo;

public class TotalDownline {
    public byte RoleId { get; set; }
    public int TotalActive { get; set; }
    public int TotalClosed { get; set; }
    public int TotalSuspended { get; set; }
    public int AgentID { get; set; }
}
