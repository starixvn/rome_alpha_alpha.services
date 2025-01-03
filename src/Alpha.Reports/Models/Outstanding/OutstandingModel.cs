namespace Alpha.Reports.Models.Outstanding;

public class OutstandingModel {
    public OutstandingModel() {

    }
    
    public OutstandingModel(int id, string name, decimal memberOutstanding, decimal outstanding, decimal? toWin, decimal? riskAmount, bool isDirectMember) {
        Id = id;
        Name = name;
        MemberOutstanding = memberOutstanding;
        Outstanding = outstanding;
        IsDirectMember = isDirectMember;
        ToWin = toWin;
        RiskAmount = riskAmount;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal MemberOutstanding { get; set; }
    public decimal Outstanding { get; set; }
    public decimal? ToWin { get; set; }
    public decimal? RiskAmount { get; set; }
    public bool IsDirectMember { get; set; }
}
 