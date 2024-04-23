using Alpha.Shared.Enumerations;

namespace Alpha.Reports.Params;

public record GetDownlineOutstandingParam(
    int CustId,
    UserLevel CustLevel,
    int DownlineId,
    string Language,
    string SiteLabel);
