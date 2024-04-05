using Alpha.Reports.Consts;

namespace Alpha.Reports.Models.TotalBets;

public class MatchModel
{
    public int LeagueId {get;set;}
    public string LeagueCode {get;set;}

    public string MatchId {get;set;}
    public string MatchCode {get;set;}
    public int HomeId {get;set;}
    public int AwayId {get;set;}
    public bool IsNeutral {get;set;}
    public int LiveIndicator {get;set;}
    public string ShowTime {get;set;}
    public DateTime GlobalShowTime {get;set;}
    public string HomeScore {get;set;}
    public string AwayScore {get;set;}
    public decimal TotalTO {get;set;}

    public string LeagueName {get;set;}
    public string HomeName {get;set;}
    public string AwayName {get;set;}
    public string FormattedTurnover => TotalTO.ToString(FormatConst.TotalBetValue);
    public string GlobalShowTime_Date => GlobalShowTime.ToString("MM/dd");
    public string GlobalShowTime_Time => GlobalShowTime.ToString("hh:mm tt");
}