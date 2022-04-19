using Alpha.Reports.Models.Outstanding;
using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetTopOutstandingQuery(int CustId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<OutstandingReport>;
