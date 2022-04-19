using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetTopOutstandingUsernameQuery(int CustId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<IEnumerable<string>>;
