namespace Alpha.Reports.Models.TotalBets;

public class TotalBetsDetailsModel
{
    public MatchModel Match {get;set;}
    public IEnumerable<BetTypeModel> BetTypes {get;set;}
    public IEnumerable<BetTypeModel> TOBetTypes {get;set;}
}