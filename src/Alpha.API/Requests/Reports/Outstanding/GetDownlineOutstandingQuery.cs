using Alpha.Reports.Models.Outstanding;
using Alpha.Shared.Enumerations;
using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetDownlineOutstandingQuery(
    int CustId,
    byte CustLevelId,
    int DownlineId,
    string Language,
    string SiteLabel,
    byte DownlineLevelId) : ReportBaseQuery(CustId, CustLevelId), IRequest<OutstandingReport> {
    internal UserLevel DownlineLevel => Enumeration.FromValue<UserLevel>(DownlineLevelId);
}
