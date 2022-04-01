using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetTopOutstandingUsernameQuery(int CustId, int SubAccId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<IEnumerable<string>>;
