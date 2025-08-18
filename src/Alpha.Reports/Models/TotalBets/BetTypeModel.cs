using Alpha.Reports.Consts;

namespace Alpha.Reports.Models.TotalBets;

public class BetTypeModel
{
    public int BetTypeId { get; set; }
    public decimal TotalTO { get; set; }
    public string FormattedTurnover => TotalTO.ToString(FormatConst.IntegerValue);

    public string HDP { get; set; }
    public string TeamHDP { get; set; }
    public decimal HomeTO { get; set; }
    public decimal AwayTO { get; set; }
    public decimal DrawTO { get; set; }
    public decimal OverTO { get; set; }
    public decimal UnderTO { get; set; }

    public string BetTypeName { get; set; }
    public string FormattedHomeTO => HomeTO.ToString(FormatConst.TotalBetValue);
    public string FormattedAwayTO => AwayTO.ToString(FormatConst.TotalBetValue);
    public string FormattedDrawTO => DrawTO.ToString(FormatConst.TotalBetValue);
    public string FormattedOverTO => OverTO.ToString(FormatConst.TotalBetValue);
    public string FormattedUnderTO => UnderTO.ToString(FormatConst.TotalBetValue);
}