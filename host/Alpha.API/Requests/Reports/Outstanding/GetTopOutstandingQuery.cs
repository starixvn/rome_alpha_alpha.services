using Alpha.Reports.Models.Outstanding;
using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetTopOutstandingQuery(int CustId, int SubAccId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<TopOutstanding>;
