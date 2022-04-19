using Alpha.Reports.Models.Outstanding;
using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetDownlineOutstandingQuery(
    int CustId,
    byte CustLevelId,
    int DownlineId,
    string Language) : ReportBaseQuery(CustId, CustLevelId), IRequest<OutstandingReport>;
