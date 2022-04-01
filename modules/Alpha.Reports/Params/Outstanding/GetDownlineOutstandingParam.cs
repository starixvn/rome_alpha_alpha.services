using Alpha.Shared.Enumerations;

namespace Alpha.Reports.Params.Outstanding;

public record GetDownlineOutstandingParam(
    int CustId,
    UserLevel CustLevel,
    int DownlineId,
    string Language);
