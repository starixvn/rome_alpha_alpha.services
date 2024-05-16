namespace Alpha.API.Client.Models.TotalBets;

public class BetTypeModel
{
    public int BetTypeId {get;set;}
    public decimal TotalTO {get;set;}
    public string FormattedTurnover {get;set;}

    public string HDP {get;set;}
    public string TeamHDP {get;set;}
    public decimal HomeTO {get;set;}
    public decimal AwayTO {get;set;}
    public decimal DrawTO {get;set;}
    public decimal OverTO {get;set;}
    public decimal UnderTO {get;set;}
    
    public string BetTypeName {get;set;}
    public string FormattedHomeTO {get;set;}
    public string FormattedAwayTO {get;set;}
    public string FormattedDrawTO {get;set;}
    public string FormattedOverTO {get;set;}
    public string FormattedUnderTO {get;set;}
}