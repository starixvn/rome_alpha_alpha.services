namespace Alpha.API.Client.Models.Outstanding;

public class OutstandingEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal MemberOutstanding { get; set; }
    public decimal Outstanding { get; set; }
    public bool IsDirectMember { get; set; }
    public decimal? ToWin { get; set; }
    public decimal? RiskAmount { get; set; }
}
 
